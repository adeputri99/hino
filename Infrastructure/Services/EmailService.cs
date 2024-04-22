﻿using SkeletonApi.Application.DTOs.Email;
using SkeletonApi.Application.Interfaces;
using System.Net.Mail;

namespace SkeletonApi.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        public async Task SendAsync(EmailRequestDto request)
        {
            var emailClient = new SmtpClient("localhost");
            var message = new MailMessage
            {
                From = new MailAddress(request.From),
                Subject = request.Subject,
                Body = request.Body
            };
            message.To.Add(new MailAddress(request.To));
            await emailClient.SendMailAsync(message);
        }
    }
}