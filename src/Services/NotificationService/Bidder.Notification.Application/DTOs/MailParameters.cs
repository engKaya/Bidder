using Bidder.Notification.Application.Abstraction;
using System.Net.Mail;

namespace Bidder.Notification.Application.DTOs
{
    public class MailParameters
    {

        public List<string> To { get; set; }
        public List<string> Cc { get; set; }
        public List<string> Bcc { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsHtml { get; set; } = false;
        public string MailFromName { get; set; }
        public string MailFromAddress { get; set; }
        public List<Attachment> Attachments { get; set; }
        
        public MailParameters(List<string> to, string subject, string body)
        {
            To = to;
            Subject = subject;
            Body = body;
        }

        public MailParameters(List<string> to, List<string> cc, string subject, string body)
        {
            To = to;
            Cc = cc;
            Subject = subject;
            Body = body;
        }

        public MailParameters(List<string> to, List<string> cc, List<string> bcc, string subject, string body)
        {
            To = to;
            Cc = cc;
            Bcc = bcc;
            Subject = subject;
            Body = body;
        }

        public MailParameters(List<string> to, List<string> cc, List<string> bcc, string subject, string body, bool isHtml)
        {
            To = to;
            Cc = cc;
            Bcc = bcc;
            Subject = subject;
            Body = body;
            IsHtml = isHtml;
        }

        public MailParameters(List<string> to, List<string> cc, List<string> bcc, string subject, string body, bool isHtml, List<Attachment> attachments)
        {
            To = to;
            Cc = cc;
            Bcc = bcc;
            Subject = subject;
            Body = body;
            IsHtml = isHtml;
            Attachments = attachments;
        }

        public MailParameters(List<string> to, List<string> cc, List<string> bcc, string subject, string body, List<Attachment> attachments)
        {
            To = to;
            Cc = cc;
            Bcc = bcc;
            Subject = subject;
            Body = body;
            Attachments = attachments;
        }

        public void AddTo(string to) => this.To.Add(to);
        public void AddCc(string cc) => this.Cc.Add(cc);
        public void AddBcc(string bcc) => this.Bcc.Add(bcc);
        public void AddAttachment(Attachment attachment) => this.Attachments.Add(attachment);
        public void AddTo(List<string> to) => this.To.AddRange(to);
        public void AddCc(List<string> cc) => this.Cc.AddRange(cc);
        public void AddBcc(List<string> bcc) => this.Bcc.AddRange(bcc);
        public void AddAttachments(List<Attachment> attachments) => this.Attachments.AddRange(attachments);
    }
}
