// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using CSSC.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System.ComponentModel;

namespace CSSC.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<CSSCUser> _signInManager;
        private readonly UserManager<CSSCUser> _userManager;
        private readonly IUserStore<CSSCUser> _userStore;
        private readonly IUserEmailStore<CSSCUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<CSSCUser> userManager,
            IUserStore<CSSCUser> userStore,
            SignInManager<CSSCUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [EmailAddress]
            [DisplayName("Email")]
            public string Email { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [StringLength(100, ErrorMessage = "A {0} tem de ter no minimo {2} caracteres e no máximo {1}.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [DisplayName("Password")]
            [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&_#^])[A-Za-z\d@$!%*?&_#^]{6,}$", ErrorMessage = "A {0} tem de ter pelo menos uma letra minúscula, uma letra maiúscula, um dígito, um caráter especial (!@#$%^_&*) e ter pelo menos 6 caracteres.")]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [DataType(DataType.Password)]
            [DisplayName("Confirmar Password")]
            [Compare("Password", ErrorMessage = "As passwords não são iguais.")]
            public string ConfirmPassword { get; set; }

            [DisplayName("Data de Nascimento")]
            public string UtDataDeNascimento { get; set; }

            [DisplayName("NIF")]
            [RegularExpression(@"^\d{9}$", ErrorMessage = "O NIF deve ter exatamente 9 dígitos.")]
            public string UtNIF { get; set; }
            
            /*Trocar o codigo de cima por este após trocar o nif para string
             * [DisplayName("NIF")]
            public string UtNIF { get; set; }*/

            [DisplayName("Morada")]
            public string UtMorada { get; set; }

            [DisplayName("Número de Telemóvel")]
            [RegularExpression(@"^9[1236]\d{7}$", ErrorMessage = "Por favor, introduza um número de telemóvel com formato válido. (iniciado por 91, 92, 93 ou 96 e com 9 digitos)")]
            public string PhoneNumber { get; set; }
        }


        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {

                var selectedCountry = Request.Form["pais"];
                if (selectedCountry == "PT")
                {
                    // Verifica se o NIF de Portugal é válido
                    if (!IsValidPortugalNIF(Input.UtNIF.ToString())) //tirar .toString() após alterar campo NIF
                    {
                        ModelState.AddModelError("Input.UtNIF", "O NIF de Portugal é inválido.");
                        return Page();
                    }
                }
                else if (selectedCountry == "ESP")
                {
                    // Verifica se o NIF de Espanha é válido
                    if (!IsValidSpainNIF(Input.UtNIF.ToString())) //tirar .toString() após alterar campo NIF
                    {
                        ModelState.AddModelError("Input.UtNIF", "O NIF de Espanha é inválido.");
                        return Page();
                    }
                }
                else if (selectedCountry == "FR")
                {
                    // Verifica se o NIF de França é válido
                    if (!IsValidFranceNIF(Input.UtNIF.ToString())) //tirar .toString() após alterar campo NIF
                    {
                        ModelState.AddModelError("Input.UtNIF", "O NIF de França é inválido.");
                        return Page();
                    }
                }

                var user = CreateUser();
                user.UtDataDeNascimento = Input.UtDataDeNascimento;
                user.UtMorada = Input.UtMorada;
                user.UtNIF = Input.UtNIF;
                user.PhoneNumber = Input.PhoneNumber;

                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    await _userManager.AddToRoleAsync(user, "Default");
                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        private bool IsValidPortugalNIF(string nif)
        {
            String s;
            char c;
            int checkDigit;

            nif = nif.Replace(" ", "");

            s = nif;

            if (s.Length == 9)
            {
                c = s[0];
                //Digitos iniciais válidos
                if (c == '1' || c == '2' || c == '5' || c == '6' || c == '8' || c == '9')
                {
                    checkDigit = (c - '0') * 9;

                    for (int i = 2; i <= 8; i++)
                        checkDigit += (s[i - 1] - '0') * (10 - i);

                    checkDigit = 11 - (checkDigit % 11);

                    if (checkDigit >= 10)
                        checkDigit = 0;

                    if (checkDigit == (s[8] - '0'))
                        return true;
                }
            }

            return false;
        }

        public static bool IsValidSpainNIF(string nif)
        {
            nif = nif.Replace(" ", "").Replace("-", "");
            // Verifica se o comprimento é 9 caracteres
            if (nif.Length != 9)
                return false;

            // Verifica se o primeiro e o último caracteres não são ambos numéricos
            if (char.IsDigit(nif[0]) && char.IsDigit(nif[8]))
                return false;

            // Verifica se o restante dos caracteres são numéricos
            for (int i = 1; i < 8; i++)
            {
                if (!char.IsDigit(nif[i]))
                    return false;
            }

            // Se passou por todas as verificações, o NIF é válido
            return true;
        }

        private bool IsValidFranceNIF(string nif)
        {
            // Lógica de validação do NIF de França

            nif = nif.Replace(" ", "").Replace("-", "");

            // Verificar o comprimento do NIF
            if (nif.Length == 13)
            {
                // Verificar se todos os caracteres são dígitos
                if (nif.All(char.IsDigit))
                {
                    // Este é um NIF válido para pessoa individual
                    return true;
                }
            }
            else if (nif.Length == 9)
            {
                // Verificar se todos os caracteres são dígitos
                if (nif.All(char.IsDigit))
                {
                    // Este é um SIREN válido para pessoa coletiva
                    return true;
                }
            }

            // Caso contrário, o NIF ou SIREN não é válido
            return false;
        }

        private CSSCUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<CSSCUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(CSSCUser)}'. " +
                    $"Ensure that '{nameof(CSSCUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<CSSCUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<CSSCUser>)_userStore;
        }
    }
}
