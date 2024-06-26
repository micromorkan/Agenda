﻿using FluentValidation;
using Agenda.Domain.Entity;
using Agenda.Domain.Interface;
using Agenda.Domain.Models;
using Agenda.Domain.Util;
using Agenda.Services.Interface;

namespace Agenda.Services.Service
{
    public class UserService : IDisposable, IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<User> _validator;

        public UserService(IUnitOfWork unitOfWork, IValidator<User> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<Result<IEnumerable<User>>> GetAll(User filter)
        {
            var userList = _unitOfWork.Users.GetAll(f => (string.IsNullOrEmpty(filter.Name) || f.Name.Contains(filter.Name)) &&
                                                       (string.IsNullOrEmpty(filter.Email) || f.Email.Contains(filter.Email)) &&
                                                       (string.IsNullOrEmpty(filter.Profile) || f.Profile == filter.Profile), null).ToList();

            userList?.ForEach(x =>
            {
                x.Password = string.Empty;
            });

            return new Result<IEnumerable<User>>
            {
                Success = true,
                Object = userList,
            };
        }

        public async Task<PaginateResult<IEnumerable<User>>> GetAllPaginate(User filter, int page, int qtPage)
        {
            var result = _unitOfWork.Users.GetAllPaginate(f => (string.IsNullOrEmpty(filter.Name) || f.Name.Contains(filter.Name)) &&
                                                         (string.IsNullOrEmpty(filter.Representation) || f.Representation.Contains(filter.Representation)) &&
                                                         (string.IsNullOrEmpty(filter.Login) || f.Login.Contains(filter.Login)) &&
                                                         (string.IsNullOrEmpty(filter.Cnpj) || f.Cnpj == filter.Cnpj), null, page, qtPage);

            result.Object?.ToList().ForEach(x =>
            {
                x.Password = string.Empty;
            });

            return result;
        }

        public async Task<Result<User>> GetById(int id)
        {
            User user = _unitOfWork.Users.GetAll(f => f.Id == id).FirstOrDefault();

            if (user != null)
            {
                user.Password = string.Empty;

                return new Result<User>
                {
                    Success = true,
                    Object = user
                };
            }
            else
            {
                return new Result<User>
                {
                    Success = false,
                    Message = Constants.SYSTEM_ERROR_ID
                };
            }
        }

        public Result<User> Insert(User user)
        {
            var resultValidation = _validator.Validate(user, options => options.IncludeRuleSets(Constants.FLUENT_INSERT));

            if (!resultValidation.IsValid)
            {
                return new Result<User>
                {
                    Success = false,
                    Object = user,
                    Errors = resultValidation.Errors
                };
            }

            user.Name = user.Name.ToUpper();
            user.Email = user.Email.ToUpper();
            user.Representation = user.Representation.ToUpper();
            user.Profile = user.Profile.ToUpper();

            _unitOfWork.Users.Insert(user);
            _unitOfWork.Users.Save();
            _unitOfWork.Dispose();

            return new Result<User>
            {
                Success = true,
                Object = user,
                Message = Constants.SYSTEM_SUCCESS_MSG
            };
        }

        public Result<User> Update(User user)
        {
            var resultValidation = _validator.Validate(user, options => options.IncludeRuleSets(Constants.FLUENT_UPDATE));

            if (!resultValidation.IsValid)
            {
                return new Result<User>
                {
                    Success = false,
                    Object = user,
                    Errors = resultValidation.Errors
                };
            }

            user.Name = user.Name.ToUpper();
            user.Email = user.Email.ToUpper();
            user.Representation = user.Representation.ToUpper();
            user.Profile = user.Profile.ToUpper();

            _unitOfWork.Users.Update(user);
            _unitOfWork.Users.Save();
            _unitOfWork.Dispose();

            return new Result<User>
            {
                Success = true,
                Object = user,
                Message = Constants.SYSTEM_SUCCESS_MSG
            };
        }

        public Result<User> Delete(int id)
        {
            User user = new User { Id = id };
            var resultValidation = _validator.Validate(user, options => options.IncludeRuleSets(Constants.FLUENT_DELETE));

            if (!resultValidation.IsValid)
            {
                return new Result<User>
                {
                    Success = false,
                    Object = user,
                    Errors = resultValidation.Errors
                };
            }

            _unitOfWork.Users.Delete(user);
            _unitOfWork.Users.Save();
            _unitOfWork.Dispose();

            return new Result<User>
            {
                Success = true,
                Object = user,
                Message = Constants.SYSTEM_SUCCESS_MSG
            };
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}