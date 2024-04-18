using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class FormBuscaCEP : Form
    {
        public FormBuscaCEP()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (txtCNPJComprador.Text == "")
            {
                MessageBox.Show("Campo CNPJ Comprador é obrigatorio");
            }
            if (txtCNPJFornecedor.Text == "")
            {
                MessageBox.Show("Campo CNPJ Fornecedor é obrigatorio");
            }
            if (txtNumeroCotacao.Text == "")
            {
                MessageBox.Show("Campo Numero Cotacao é obrigatorio");
            }
            if (txtDataCotacao.Text == "")
            {
                MessageBox.Show("Campo Data Cotacao é obrigatorio");
            }
            if (txtDataEntregaCotacao.Text == "")
            {
                MessageBox.Show("Campo Data Entrega Cotacao é obrigatorio");
            }
            if (txtCep.Text == "")
            {
                MessageBox.Show("Campo CEP é obrigatorio");
            }
            if (txtQuantidade.Text == "")
            {
                MessageBox.Show("Campo Quantidade é obrigatorio");
            }
            if (txtNumeroItem.Text == "")
            {
                MessageBox.Show("Campo Numero Item é obrigatorio");
            }
            if (txtDescricaoItem.Text == "")
            {
                MessageBox.Show("Campo Descrição Item é obrigatorio");
            }
            else
            {
                var cep = txtCep.Text;
                var endereco = await BuscarCEPAsync(cep);
                if (txtLogradouro.Text != "")
                {
                    if(txtUF.Text != "")
                    {
                        if (txtBairro.Text != "")
                        {
                            if(txtComplemento.Text != "")
                            {
                                return;
                            }
                            else
                            {
                                txtComplemento.Text = endereco.complemento;
                            }
                        }
                        else
                        {
                            txtBairro.Text = endereco.bairro;
                        }

                    }
                    else
                    {
                        txtUF.Text = endereco.uf;
                    }
                }
                else
                {
                    txtLogradouro.Text = endereco.logradouro;
                }
            }
        }
        private async Task<Endereco> BuscarCEPAsync(string cep)
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    var response = await httpClient.GetAsync($"https://viacep.com.br/ws/{cep}/json/");

                    if (response.IsSuccessStatusCode)
                    {

                        var content = await response.Content.ReadAsStringAsync();
                        var endereco = JsonSerializer.Deserialize<Endereco>(content);

                        return endereco;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao buscar CEP: {ex.Message}");
                    return null;
                }
            }
        }
        }
    }

