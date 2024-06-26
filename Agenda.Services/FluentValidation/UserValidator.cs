﻿using FluentValidation;
using Agenda.Domain.Interface;
using Agenda.Domain.Util;

namespace Agenda.Services.FluentValidation
{
    public class UserValidator : AbstractValidator<Domain.Entity.User>
    {
        private IUnitOfWork _unitOfWork;

        public UserValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleSet(Constants.FLUENT_INSERT, () =>
            {
                RuleFor(x => x.Name).NotEmpty().WithMessage("O nome é obrigatório.");
                RuleFor(x => x.Cnpj).NotEmpty().Must(x => Functions.IsCnpj(x)).WithMessage("Informe um cnpj válido.");
                RuleFor(x => x.Representation).NotEmpty().WithMessage("O representante é obrigatório.");
                RuleFor(x => x.Login).NotEmpty().WithMessage("O login é obrigatório.");
                RuleFor(x => x.Password).NotEmpty().WithMessage("A senha é obrigatória.");
                RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Informe um email válido.");
                RuleFor(x => x.Profile).NotEmpty().WithMessage("O perfil é obrigatório.");
                RuleFor(x => x).Custom(InsertUniqueCnpjLoginNameEmail);
            });

            RuleSet(Constants.FLUENT_UPDATE, () =>
            {
                RuleFor(x => x.Id).GreaterThan(0).WithMessage("O id do usuário é inválido.");
                RuleFor(x => x.Name).NotEmpty().WithMessage("O nome é obrigatório.");
                RuleFor(x => x.Cnpj).NotEmpty().Must(x => Functions.IsCnpj(x)).WithMessage("Informe um cnpj válido.");
                RuleFor(x => x.Representation).NotEmpty().WithMessage("O representante é obrigatório.");
                RuleFor(x => x.Login).NotEmpty().WithMessage("O login é obrigatório.");
                RuleFor(x => x.Password).NotEmpty().WithMessage("A senha é obrigatória.");
                RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Informe um email válido.");
                RuleFor(x => x.Profile).NotEmpty().WithMessage("O perfil é obrigatório.");
                RuleFor(x => x).Custom(EditUniqueCnpjLoginNameEmail);
            });

            RuleSet(Constants.FLUENT_DELETE, () =>
            {
                RuleFor(x => x.Id).GreaterThan(0).WithMessage("O id do usuário é inválido.");
                //RuleFor(x => x).Custom(DeleteBlockRole);
            });

            RuleSet(Constants.FLUENT_AUTHENTICATE, () =>
            {
                RuleFor(x => x.Login).NotEmpty().WithMessage("O Login é obrigatório.");
                RuleFor(x => x.Password).NotEmpty().WithMessage("A Senha é obrigatória.");
            });
        }

        private void InsertUniqueCnpjLoginNameEmail(Domain.Entity.User model, ValidationContext<Domain.Entity.User> context)
        {
            var result = _unitOfWork.Users.GetAll(filter => filter.Cnpj == model.Cnpj || 
                                                            filter.Name == model.Name || 
                                                            filter.Login == model.Login ||
                                                            filter.Email == model.Email);

            if (result.Any())
            {
                var existUser = result.First();

                if (existUser.Cnpj == model.Cnpj && model.Profile != Constants.PROFILE_DIRETOR)
                {
                    context.AddFailure("Já existe um Cnpj cadastrado");
                }
                
                if (existUser.Name == model.Name)
                {
                    context.AddFailure("Já existe um Nome cadastrado");
                }
                
                if (existUser.Login == model.Login)
                {
                    context.AddFailure("Já existe um Login cadastrado");
                }

                if (existUser.Email == model.Email)
                {
                    context.AddFailure("Já existe um Email cadastrado");
                }
            }
        }

        private void EditUniqueCnpjLoginNameEmail(Domain.Entity.User model, ValidationContext<Domain.Entity.User> context)
        {
            var result = _unitOfWork.Users.GetAll(filter => (filter.Cnpj == model.Cnpj ||
                                                            filter.Name == model.Name ||
                                                            filter.Login == model.Login ||
                                                            filter.Email == model.Email) &&
                                                            filter.Id != model.Id);

            if (result.Any())
            {
                var existUser = result.First();

                if (existUser.Cnpj == model.Cnpj && model.Profile != Constants.PROFILE_DIRETOR)
                {
                    context.AddFailure("Já existe um Cnpj cadastrado");
                }

                if (existUser.Name == model.Name)
                {
                    context.AddFailure("Já existe um Nome cadastrado");
                }

                if (existUser.Login == model.Login)
                {
                    context.AddFailure("Já existe um Login cadastrado");
                }

                if (existUser.Email == model.Email)
                {
                    context.AddFailure("Já existe um Email cadastrado");
                }
            }
        }
    }
}
