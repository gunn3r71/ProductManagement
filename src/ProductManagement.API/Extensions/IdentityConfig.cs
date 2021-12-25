using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductManagement.API.Data;

namespace ProductManagement.API.Extensions
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddIdentity<IdentityUser, IdentityRole>(x =>
                {
                    x.Password = new()
                    {
                        RequireDigit = true,
                        RequireLowercase = true,
                        RequireUppercase = true,
                        RequiredLength = 8,
                        RequireNonAlphanumeric = true
                    };
                    x.User = new()
                    {
                        RequireUniqueEmail = true,
                    };
                })
                .AddErrorDescriber<MessagesConfig>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }

        public class MessagesConfig : IdentityErrorDescriber
        {
            public override IdentityError DefaultError()
            {
                return new()
                {
                    Code = nameof(DefaultError),
                    Description = "Ocorreu um erro desconhecido."
                };
            }

            public override IdentityError ConcurrencyFailure()
            {
                return new()
                {
                    Code = nameof(ConcurrencyFailure),
                    Description = "O objeto foi alterado."
                };
            }

            public override IdentityError DuplicateEmail(string email)
            {
                return new()
                {
                    Code = nameof(DuplicateEmail),
                    Description = $"O email {email} já existe."
                };
            }

            public override IdentityError DuplicateRoleName(string role)
            {
                return new()
                {
                    Code = nameof(DuplicateRoleName),
                    Description = $"O nível {role} já existe."
                };
            }

            public override IdentityError DuplicateUserName(string userName)
            {
                return new()
                {
                    Code = nameof(DuplicateUserName),
                    Description = $"O usuário {userName} já existe."
                };
            }

            public override IdentityError InvalidEmail(string email)
            {
                return new()
                {
                    Code = nameof(InvalidEmail),
                    Description = $"O email {email} está num formato inválido."
                };
            }

            public override IdentityError InvalidRoleName(string role)
            {
                return new()
                {
                    Code = nameof(InvalidRoleName),
                    Description = $"O nível {role} é inválido."
                };
            }

            public override IdentityError InvalidToken()
            {
                return new()
                {
                    Code = nameof(InvalidToken),
                    Description = "O token é inválido."
                };
            }

            public override IdentityError InvalidUserName(string userName)
            {
                return new()
                {
                    Code = nameof(InvalidUserName),
                    Description = $"O username {userName} é inválido."
                };
            }

            public override IdentityError PasswordMismatch()
            {
                return new()
                {
                    Code = nameof(PasswordMismatch),
                    Description = "usuário ou senha invalidos."
                };
            }

            public override IdentityError PasswordTooShort(int length)
            {
                return new()
                {
                    Code = nameof(PasswordTooShort),
                    Description = $"A senha deve conter {length} caracteres."
                };
            }

            public override IdentityError PasswordRequiresUpper()
            {
                return new()
                {
                    Code = nameof(PasswordRequiresUpper),
                    Description = "A senha deve conter um caractere em maíusculo."
                };
            }

            public override IdentityError PasswordRequiresDigit()
            {
                return new()
                {
                    Code = nameof(PasswordRequiresDigit),
                    Description = "A senha deve conter um número."
                };
            }

            public override IdentityError PasswordRequiresLower()
            {
                return new()
                {
                    Code = nameof(PasswordTooShort),
                    Description = "A senha deve conter um caractere em maíusculo."
                };
            }

            public override IdentityError PasswordRequiresNonAlphanumeric()
            {
                return new()
                {
                    Code = nameof(PasswordRequiresNonAlphanumeric),
                    Description = "A senha deve conter um caractere especial."
                };
            }

            public override IdentityError PasswordRequiresUniqueChars(int uniqueChars)
            {
                return new()
                {
                    Code = nameof(PasswordRequiresNonAlphanumeric),
                    Description = $"A senha deve conter {uniqueChars} caracteres especiais."
                };
            }
        }
    }
}