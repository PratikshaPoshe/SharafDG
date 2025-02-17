﻿using AutoMapper;
using MediatR;
using SharafDG.Application.Contracts.Persistence;
using SharafDG.Application.Responses;
using SharafDG.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SharafDG.Application.Contracts.Infrastructure;
using SharafDG.Application.Models.Mail;

namespace SharafDG.Application.Features.SME.Commands.CreateSME
{
   public class CreateSMECommandHandler : IRequestHandler<CreateSMECommand, Response<CreateSMEDto>>
    {
        private readonly IAsyncRepository<SMEUser> _smeRepository;
        private readonly IMapper _mapper;
        private readonly IMailService email;
        public CreateSMECommandHandler(IMapper mapper, IAsyncRepository<SMEUser> smeRepository, IMailService email)
        {
            _mapper = mapper;
            _smeRepository = smeRepository;
            this.email = email;
        }
        public async Task<Response<CreateSMEDto>> Handle(CreateSMECommand request, CancellationToken cancellationToken)
        {
            var createSMECommandResponse = new Response<CreateSMEDto>();

            var validator = new CreateSMECommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
            {
                createSMECommandResponse.Succeeded = false;
                createSMECommandResponse.Errors = new List<string>();
                foreach (var error in validationResult.Errors)
                {
                    //CreateSMECommandResponse.Errors.Add(error.ErrorMessage);
                }
            }
            else
            {
                var sme = new SMEUser()
                {
                    Name = request.Name,
                    Email = request.Email,
                    Password = request.Password,
                    PhoneNumber = request.PhoneNumber,
                    CompanyName = request.CompanyName,
                    CompanyAddress = request.CompanyAddress,
                    CompanyPhone = request.CompanyPhone,
                    CompanyWebsite = request.CompanyWebsite,
                    AlternateNumber = request.AlternateNumber,
                    TypeOfBusiness = request.TypeOfBusiness
                };

                sme = await _smeRepository.AddAsync(sme);
                createSMECommandResponse.Data = _mapper.Map<CreateSMEDto>(sme);
                createSMECommandResponse.Succeeded = true;
                createSMECommandResponse.Message = "success";

               
            }
            return createSMECommandResponse;
        }
        /*{
    "name": "aman",
    "email": "rjamans18@gmail.com",
    "password": "Aman123",
    "typeOfBusiness": "private",
    "companyName": "aman",
    "companyAddress": "xyz",
    "companyPhone": 123450,
    "companyWebsite": "aman.com",
    "phoneNumber": 9999999999,
    "alternateNumber": 0
  }*/
    }
}
