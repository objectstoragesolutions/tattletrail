using MimeKit;
using System;
using System.Collections.Generic;

namespace TattleTrail.Infrastructure.Extensions {
    public static class StringExtensions {
        public static IEnumerable<MailboxAddress> ToMailBoxArray(this String[] emailsArray) {
            HashSet<MailboxAddress> emails = new HashSet<MailboxAddress>();
                foreach (var email in emailsArray) {
                    emails.Add(new MailboxAddress(email));
                }

            return emails;
        }
    }
}
