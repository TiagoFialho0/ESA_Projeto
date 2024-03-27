// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener('DOMContentLoaded', function () {
    var toggleButtons = document.querySelectorAll('.toggle-details-btn');

    toggleButtons.forEach(function (button) {
        button.addEventListener('click', function () {
            var row = this.closest('.expandable-row');
            var hiddenRow = row.nextElementSibling;
            hiddenRow.classList.toggle('show-details');
        });
    });
});

//Função que valida os campos nif e data de nascimento do registo de utilizador
function validarCampos() {

    // Array para armazenar os nomes dos campos vazios
    var camposVazios = [];

    // Obtém o formulário de registro
    var form = document.getElementById('registerForm');

    // Mapeia os nomes dos campos para seus DisplayNames
    var camposDisplayNames = {
        'Input.Email': 'Email',
        'Input.Password': 'Password',
        'Input.ConfirmPassword': 'Confirmar Password',
        'Input.UtNIF': 'NIF',
        'Input.UtDataDeNascimento': 'Data de Nascimento',
        'Input.UtMorada': 'Morada',
        'Input.PhoneNumber': 'Número de Telemóvel'
    };

    // Seleciona todos os inputs dentro do formulário
    var inputs = form.querySelectorAll('input');

    // Verifica cada input
    inputs.forEach(function (input) {
        // Verifica se o campo está vazio ou só contém espaços em branco
        if (input.value.trim() === '') {
            // Obtém o DisplayName do campo
            var displayName = camposDisplayNames[input.getAttribute('name')];
            // Adiciona o DisplayName do campo à lista de campos vazios
            if (displayName) {
                camposVazios.push(displayName);
            } else {
                // Se o DisplayName não for encontrado, adiciona o nome do campo ao invés disso
                camposVazios.push(input.getAttribute('name'));
            }
        }
    });


    // Se houver campos vazios, exibe um alerta com os nomes desses campos
    if (camposVazios.length > 0) {
        alert('Os seguintes campos estão vazios:\n' + camposVazios.join('\n') + '\nPor favor, preencha!');
        return false; // Impede o envio do formulário
    }

    var nifInput = document.getElementById('nifInput');
    var nifValue = nifInput.value;

    //Verifica se nif inserido tem tamanho diferente de 9
    if (nifValue.length !== 9) {
        alert('O NIF deve ter 9 dígitos.');
        return false; // Impede o envio do formulário
    }
    var dataNascimento = new Date(document.getElementById("dataNascimentoInput").value);
    var hoje = new Date();
    var idadeMinima = 18;

    // Calcula a idade
    var idade = hoje.getFullYear() - dataNascimento.getFullYear();
    var mesAtual = hoje.getMonth();
    var diaAtual = hoje.getDate();
    var mesNascimento = dataNascimento.getMonth();
    var diaNascimento = dataNascimento.getDate();

    // Verifica se a pessoa tem menos de 18 anos
    var menorDeIdade = idade < idadeMinima || (idade === idadeMinima && (mesAtual < mesNascimento || (mesAtual === mesNascimento && diaAtual < diaNascimento)));

    // Se a pessoa for menor de idade, impede o envio do formulário e exibe um alerta
    if (menorDeIdade) {
        event.preventDefault();
        alert("Deverá ter pelo menos 18 anos.");
    }

    return true; // Permite o envio do formulário
}



//Função que verifica a data inserida para notificação de serviço ao cliente
function validarAgendarNotif() {
    const dataAtual = new Date();
    const dataInserida = new Date(document.getElementById("DataInicial").value);

    // Adiciona 1 hora à data atual
    dataAtual.setHours(dataAtual.getHours() + 1);

    //Verifica se a data inserida é inferior à data atual
    if (dataInserida < dataAtual) {
        alert("Data e hora devem ser pelo menos 1 hora após a data atual.");
        return false;
    } else {
        //console.log('Data e hora válidas.');
        return true;
    }
}

function togglePasswordVisibility(page) {
    var passwordInput;
    var passwordIcon;

    if (page === 'register') {
        passwordInput = document.getElementById('passwordInput');
        passwordIcon = document.getElementById('registerPasswordIcon');
    } else if (page === 'login') {
        passwordInput = document.getElementById('loginPasswordInput');
        passwordIcon = document.getElementById('loginPasswordIcon');
    }

    if (passwordInput.type === 'password') {
        passwordInput.type = 'text';
        passwordIcon.classList.remove('fa-eye');
        passwordIcon.classList.add('fa-eye-slash');
    } else {
        passwordInput.type = 'password';
        passwordIcon.classList.remove('fa-eye-slash');
        passwordIcon.classList.add('fa-eye');
    }
}

function updatePasswordStrength() {
    var password = document.getElementById('passwordInput').value;
    var strengthBar = document.getElementById('strengthBar');
    var progressBarWidth = 0;

    // Check for length
    if (password.length >= 6) {
        progressBarWidth += 20;
        document.querySelector('.eight-character').classList.add('text-success');
    } else {
        document.querySelector('.eight-character').classList.remove('text-success');
    }

    // Check for lowercase letters
    if (/[a-z]/.test(password)) {
        progressBarWidth += 20;
        document.querySelector('.low-case').classList.add('text-success');
    } else {
        document.querySelector('.low-case').classList.remove('text-success');
    }

    // Check for uppercase letters
    if (/[A-Z]/.test(password)) {
        progressBarWidth += 20;
        document.querySelector('.upper-case').classList.add('text-success');
    } else {
        document.querySelector('.upper-case').classList.remove('text-success');
    }

    // Check for numbers
    if (/\d/.test(password)) {
        progressBarWidth += 20;
        document.querySelector('.one-number').classList.add('text-success');
    } else {
        document.querySelector('.one-number').classList.remove('text-success');
    }

    // Check for special characters
    if (/[^a-zA-Z0-9]/.test(password)) {
        progressBarWidth += 20;
        document.querySelector('.one-special-char').classList.add('text-success');
    } else {
        document.querySelector('.one-special-char').classList.remove('text-success');
    }

    strengthBar.style.width = progressBarWidth + "%";
    strengthBar.setAttribute('aria-valuenow', progressBarWidth);

    if (progressBarWidth < 50) {
        strengthBar.classList.remove('bg-success');
        strengthBar.classList.add('bg-danger');
    } else if (progressBarWidth >= 50 && progressBarWidth < 75) {
        strengthBar.classList.remove('bg-danger');
        strengthBar.classList.add('bg-warning');
    } else {
        strengthBar.classList.remove('bg-warning');
        strengthBar.classList.add('bg-success');
    }
}

document.getElementById('passwordInput').addEventListener('input', function () {
    updatePasswordStrength();
});