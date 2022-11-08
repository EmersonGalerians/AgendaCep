using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Agenda_Lib.DAO.CEP
{
     public class DadosCEP
     {
        string ChaveConexao = "Data Source=10.39.45.44;Initial Catalog=Senac2022;Persist Security Info=True;User ID=Turma2022;Password=Turma2022@2022";
        public DataSet List_cep(string p_CEP)
        {
            DataSet DataSetCEP = new DataSet();
            try
            {
                SqlConnection Conexao = new SqlConnection(ChaveConexao);
                Conexao.Open();
                string wQuery = $"select * from Customer_List where Name like '%{p_CEP}%'";
                SqlDataAdapter adapter = new SqlDataAdapter(wQuery, Conexao);
                adapter.Fill(DataSetCEP);
                Conexao.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return DataSetCEP;
        }
        public void Apagar_CEP(string p_CEP)
        {
            try
            {
                SqlConnection Conexao = new SqlConnection(ChaveConexao);
                Conexao.Open();
                String oQueryDelete = $"delete from where ceo = '{p_CEP}";
                SqlCommand Cmd = new SqlCommand(oQueryDelete, Conexao);
                Conexao.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Alterar_CEP(
                      String p_cep, 
                      String p_logradouro,
                      String p_complemento,
                      String p_bairro,
                      String p_localidade,  
                      String p_uf,
                      String p_ibge,  
                      String p_gia,
                      String p_ddd,
                      String p_siafi
            )
        {
            try
            {
                SqlConnection Conexao = new SqlConnection(ChaveConexao);
                Conexao.Open();
                String oQueryUpdate = 
                      $"`Update cep "     +
                      $",logradouro = '{ logradouro ^}'" +
                      $",complemento = {complemento}"    +
                      $",bairro =      {bairro}"              +
                      $",localidade =  {localidade}"      +
                      $",uf =          {uf}"                      +
                      $",ibge =        {ibge}"                  +
                      $",gia =         {gia}"                    +
                      $",ddd =         {ddd}"                    +
                      $",siafi =       {siafi}";
                SqlCommand Cmd = new SqlCommand(oQueryUpdate, Conexao);
                Conexao.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public VIACep Pesquisar_CEP(string p_CEP) {
            VIACep ViaCEP = new VIACep();

            try {
                HttpClient _httpClient = new HttpClient();
                HttpResponseMessage result =
                    _httpClient.GetAsync(oURL).Result;

                String JsonRetorno =
                       result.Content.ReadAsStringAsync().Result;
                ViaCEP oviaCEP = new ViaCEP();
                oviaCEP = JsonConvert.DeserializeObject<ViaCEP>(JsonRetorno);
                return oviaCEP;
            }
            catch (Exception){ }
        }

        public void add_cep(VIACep oViaCEP) {
            DataSet DataSetPesquisa = new DataSet();
            DataSetPesquisa = List_CEP(oViaCEP.cep);

            if (DataSetPesquisa.Tables[0].Rows.Count == 0) {
                adicionar_CEP(
                    oViaCEP.cep,
                    oViaCEP.logradouro,
                    oViaCEP.complemento,
                    oViaCEP.bairro,
                    oViaCEP.uf,
                    oViaCEP.ibge,
                    oViaCEP.gia
                    oViaCEP.ddd,
                    oViaCEP.siafi
                    );
            }
            else {
                Console.WriteLine($"Ja existe dados para este CEP {oViaCEP.Cep}quantidade de registro{DataSetPesquisa.Tables[0].Rows.Count}");
            }
        }
     }

    
}
