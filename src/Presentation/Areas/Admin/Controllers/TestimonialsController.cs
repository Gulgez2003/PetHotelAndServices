namespace FinalProjectApp.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TestimonialsController : Controller
    {
        private readonly ISender _sender;

        public TestimonialsController(ISender sender)
        {
            _sender = sender;
        }

        public async Task<IActionResult> Index()
        {
            var testimonials = await _sender.Send(new GetCommentsQuery());
            return View(testimonials);
        }

        public async Task<IActionResult> RejectedComments()
        {
            List<TestimonialGetDTO> testimonials = await _sender.Send(new GetRejectedCommentsQuery());
            return View(testimonials);
        }

        public async Task<IActionResult> PublishedComments(int id)
        {
            List<TestimonialGetDTO> testimonials = await _sender.Send(new GetPublishedCommentsQuery());
            return View(testimonials);
        }

        public async Task<IActionResult> Approve(int id)
        {
            var testimonial = await _sender.Send(new GetCommentByIdQuery { Id = id});
            if (testimonial == null)
            {
                return NotFound();
            }

            var apiKeyValue = "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";
            Environment.SetEnvironmentVariable("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", apiKeyValue);

            var apiKey = Environment.GetEnvironmentVariable("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");

            if (string.IsNullOrWhiteSpace(apiKey))
            {
                Console.WriteLine("SendGrid API key is missing or invalid.");
            }
            else
            {
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress("zoosfera7@gmail.com", "ZooSfera");
                var subject = "Your Comment Has Been Published!";
                var to = new EmailAddress(testimonial.Email, "Example User");
                var plainTextContent = "Your Comment Has Been Published!";
                var htmlContent = "";

                using (StreamReader stream = new StreamReader("wwwroot/assets/HtmlTemplates/approvedComment.html"))
                {
                    htmlContent = stream.ReadToEnd();
                }

                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

                var response = await client.SendEmailAsync(msg);

                if (response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.Accepted)
                {
                    Console.WriteLine("Email sent successfully.");
                }
                else
                {
                    Console.WriteLine($"Error sending email. Status code: {response.StatusCode}");

                }
            }

            await _sender.Send(new ApproveCommentCommand { Id = id });

            return Json(new { status = 200, message = "Comment has been published." });
        }

        public async Task<IActionResult> Disapprove(int id)
        {
            var testimonial = await _sender.Send(new GetCommentByIdQuery { Id = id });
            if (testimonial == null)
            {
                return NotFound();
            }

            var apiKeyValue = "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";
            Environment.SetEnvironmentVariable("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", apiKeyValue);

            var apiKey = Environment.GetEnvironmentVariable("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");

            if (string.IsNullOrWhiteSpace(apiKey))
            {
                Console.WriteLine("SendGrid API key is missing or invalid.");
            }
            else
            {
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress("zoosfera7@gmail.com", "ZooSfera");
                var subject = "Your Comment Has Been Rejected!";
                var to = new EmailAddress(testimonial.Email, "Example User");
                var plainTextContent = "Your Comment Has Been Rejected!";
                var htmlContent = "";

                using (StreamReader stream = new StreamReader("wwwroot/assets/HtmlTemplates/disapprovedComment.html"))
                {
                    htmlContent = stream.ReadToEnd();
                }

                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

                var response = await client.SendEmailAsync(msg);

                if (response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.Accepted)
                {
                    Console.WriteLine("Email sent successfully.");
                }
                else
                {
                    Console.WriteLine($"Error sending email. Status code: {response.StatusCode}");

                }
            }

            await _sender.Send(new DisapproveCommentCommand { Id = id });

            return Json(new { status = 200, message = "Comment has been rejected." });
        }
    }
}
