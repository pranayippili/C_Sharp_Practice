

Office.onReady(() => {
  Office.actions.associate("autorunMeeting", autorunMeeting);
});

function autorunMeeting(event) {
  const item = Office.context.mailbox.item;

  if (item && item.itemType === Office.MailboxEnums.ItemType.Appointment) {
    console.log("OnNewAppointmentCompose triggered!");
    startMeetingPolling(item);
  }

  event.completed(); // Mandatory
}

function startMeetingPolling(item) {
  let previousRecipients = [];
  let previousSubject = "";
  let notificationShown = false;
  let previousDurationMinutes = 0;

  const meetingCostMap = {
    "pranayippili@gmail.com": 6,
    "outlook_58223238ceab3d21@outlook.com": 3,
    "amogh_banerjee@outlook.com": 4,
    "amoghbanerjee@outlook.com": 5,
    "soundarraj@outlook.com": 9
  };
  const defaultCost = 50;

  const intervalId = setInterval(async () => {
    try {
      const subject = await getSubjectAsync(item);
      const allRecipients = await getAllRecipients(item);
      const durationMinutes = await getMeetingDurationInMinutes(item);

      console.log("Subject:", subject);
      console.log("Recipients:", allRecipients);
      console.log("Duration (minutes):", durationMinutes);

      const isTownHall = subject.toLowerCase().includes("town hall");
      const recipientsChanged = !arraysEqual(allRecipients, previousRecipients);
      const subjectChanged = previousSubject.toLowerCase() !== subject.toLowerCase();
      const durationMinutesChanged = previousDurationMinutes !== (await getMeetingDurationInMinutes(item));

      // Save previous state
      previousRecipients = allRecipients;
      previousSubject = subject;
      previousDurationMinutes = durationMinutes;

      // Business rule: ≥ 1 attendee, duration ≥ 30, and not town hall
      if ((recipientsChanged || subjectChanged || durationMinutesChanged) && allRecipients.length >= 1 && durationMinutes >= 30 && !isTownHall) {
        let totalCost = 0;
        const recipientCosts = allRecipients.map(email => {
          const cost = meetingCostMap[email] || defaultCost;
          totalCost += cost;
          return `${email}: $${cost}`;
        });

        showNotification(
          "Meeting cost update",
          `Total: $${totalCost}\n${recipientCosts.join("\n")}`
        );
        notificationShown = true;

      } else if (notificationShown && (allRecipients.length < 1 || durationMinutes < 30 || isTownHall)) {
        removeNotification();
        notificationShown = false;
      }

    } catch (error) {
      console.error("Error in polling loop:", error);
    }
  }, 3000);

  item.addHandlerAsync(Office.EventType.ItemSend, () => {
    clearInterval(intervalId);
    console.log("Meeting sent — polling stopped.");
  });
}

// ---------------- Helper Functions ----------------

async function getMeetingDurationInMinutes(item) {
  const start = await getDateTimeAsync(item.start);
  const end = await getDateTimeAsync(item.end);

  if (!start || !end) return 0;
  return (end - start) / (1000 * 60);
}

function getDateTimeAsync(officeDateTime) {
  return new Promise((resolve, reject) => {
    if (!officeDateTime || !officeDateTime.getAsync) return resolve(null);
    officeDateTime.getAsync(result => {
      if (result.status === Office.AsyncResultStatus.Succeeded) {
        resolve(new Date(result.value));
      } else {
        reject(result.error);
      }
    });
  });
}

async function getAllRecipients(item) {
  const required = await getRecipientsAsync(item.requiredAttendees);
  const optional = await getRecipientsAsync(item.optionalAttendees);
  return [...required, ...optional];
}

function getRecipientsAsync(recipientCollection) {
  return new Promise((resolve, reject) => {
    if (!recipientCollection || !recipientCollection.getAsync) return resolve([]);
    recipientCollection.getAsync(result => {
      if (result.status === Office.AsyncResultStatus.Succeeded) {
        const emails = (result.value || []).map(r => r.emailAddress || r.displayName);
        resolve(emails);
      } else reject(result.error);
    });
  });
}

function getSubjectAsync(item) {
  return new Promise((resolve, reject) => {
    item.subject.getAsync(result => {
      if (result.status === Office.AsyncResultStatus.Succeeded) {
        resolve(result.value || "");
      } else reject(result.error);
    });
  });
}

function arraysEqual(a, b) {
  if (!a || !b) return false;
  if (a.length !== b.length) return false;
  const sortedA = [...a].sort();
  const sortedB = [...b].sort();
  return sortedA.every((v, i) => v === sortedB[i]);
}

function showNotification(header, content) {
  Office.context.mailbox.item.notificationMessages.replaceAsync("recipientsUpdate", {
    type: "informationalMessage",
    icon: "Icon.16x16",
    message: `${header} - ${content}`,
    persistent: false
  });
}

function removeNotification() {
  Office.context.mailbox.item.notificationMessages.removeAsync("recipientsUpdate", result => {
    if (result.status === Office.AsyncResultStatus.Succeeded) {
      console.log("Notification removed.");
    } else {
      console.warn("Could not remove notification:", result.error);
    }
  });
}
