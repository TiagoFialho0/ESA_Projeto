﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
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