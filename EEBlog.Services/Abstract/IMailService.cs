using EEBlog.Entities.Dtos;
using EEBlog.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EEBlog.Services.Abstract
{
    public interface IMailService
    {
        IResult Send(EmailSendDto emailSendDto);
        IResult SendContactEmail(EmailSendDto emailSendDto);
    }
}
