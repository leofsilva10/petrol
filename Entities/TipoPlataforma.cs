using System;
using System.Data;
using System.Data.OracleClient;

namespace Petrol.Entities
{
    public class TipoPlataforma
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Lamina { get; set; }
        public int Perfuracao { get; set; }
        public int Producao { get; set; }
        public int ControlePocos { get; set; }
        public string Escoamento { get; set; }
        public string Vantagem { get; set; }
        public string Imagem { get; set; }

        public List<TipoPlataforma> ListarTiposPlataformas(int codidoTipoPlataforma, string nomeTipoPlataforma)
        {
            // Definindo a lista de tipos de Plataformas (com base na classe).
            List<TipoPlataforma> listaTiposPlataformas = new List<TipoPlataforma>();

            // Obtendo a string de conexão do arquivo appsettings.json e conectando-se ao banco de dados.
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json");
            var config = builder.Build();

            var connectionStringOracle = config.GetConnectionString("Oracle");
            OracleConnection conn = new OracleConnection(connectionStringOracle);
            conn.Open();

            // Definindo a stored procedure a ser executada para obter a lista de tipos de plataformas.
            OracleCommand cmd = new OracleCommand("PKG_TIPO_PLATAFORMA.SP_LISTA_TIPOS_PLATAFORMAS", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // Definindo os parâmetros de entrada na stored procedure.

            OracleParameter param1_in = new OracleParameter("P_TIPL_CD_CODIGO", OracleType.Int32);
            param1_in.Direction = ParameterDirection.Input;
            param1_in.Value = codidoTipoPlataforma;
            cmd.Parameters.Add(param1_in);

            OracleParameter param2_in = new OracleParameter("P_TIPL_TX_NOME", OracleType.VarChar);
            param2_in.Direction = ParameterDirection.Input;
            param2_in.Value = nomeTipoPlataforma;
            cmd.Parameters.Add(param2_in);

            // Definindo o parâmetro de saída (cursor) da stored procedure.
            OracleParameter output = cmd.Parameters.Add("Cursor_Tipos_Plataformas", OracleType.Cursor);
            output.Direction = ParameterDirection.ReturnValue;

            cmd.ExecuteNonQuery();

            OracleDataReader reader = cmd.ExecuteReader();

            // Lendo os dados retornados e adicionando-os à lista de tipos de Plataformas.
            while (reader.Read())
            {
                TipoPlataforma tipoPlataforma = new TipoPlataforma();
                tipoPlataforma.Codigo = reader.GetInt32(reader.GetOrdinal("TIPL_CD_CODIGO"));
                tipoPlataforma.Nome = reader.GetString(reader.GetOrdinal("TIPL_TX_NOME"));
                try
                {
                    tipoPlataforma.Descricao = reader.GetString(reader.GetOrdinal("TIPL_TX_DESCRICAO"));
                }
                catch
                {
                    tipoPlataforma.Descricao = String.Empty;
                }
                try
                {
                    tipoPlataforma.Lamina = reader.GetString(reader.GetOrdinal("TIPL_TX_LAMINA"));
                }
                catch
                {
                    tipoPlataforma.Lamina = String.Empty;
                }
                tipoPlataforma.Perfuracao = reader.GetInt32(reader.GetOrdinal("TIPL_IN_PERFURACAO"));
                tipoPlataforma.Producao = reader.GetInt32(reader.GetOrdinal("TIPL_IN_PRODUCAO"));
                tipoPlataforma.ControlePocos = reader.GetInt32(reader.GetOrdinal("TIPL_IN_CONTROLE_POCOS"));
                try
                {
                    tipoPlataforma.Escoamento = reader.GetString(reader.GetOrdinal("TIPL_TX_ESCOAMENTO"));
                }
                catch
                {
                    tipoPlataforma.Escoamento = String.Empty;
                }
                try
                {
                    tipoPlataforma.Vantagem = reader.GetString(reader.GetOrdinal("TIPL_TX_VANTAGEM"));
                }
                catch
                {
                    tipoPlataforma.Imagem = String.Empty;
                }
                try
                {
                    tipoPlataforma.Imagem = reader.GetString(reader.GetOrdinal("TIPL_TX_IMAGEM"));
                }
                catch
                {
                    tipoPlataforma.Imagem = String.Empty;
                }

                listaTiposPlataformas.Add(tipoPlataforma);
            }

            conn.Close();

            // Retornando a lista de tipos de Plataformas.
            return listaTiposPlataformas;
        }

        public string Adicionar(TipoPlataforma infoTipoPlataforma)
        {
            // Obtendo a string de conexão do arquivo appsettings.json e conectando-se ao banco de dados.
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json");
            var config = builder.Build();

            var connectionStringOracle = config.GetConnectionString("Oracle");
            OracleConnection conn = new OracleConnection(connectionStringOracle);
            conn.Open();

            // Definindo a stored procedure para adicionar o registro.
            OracleCommand cmd = new OracleCommand("PKG_TIPO_PLATAFORMA.SP_ADICIONAR_TIPO_PLATAFORMA", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // Definindo os parâmetros de entrada da stored procedure.

            OracleParameter param1_in = new OracleParameter("P_TIPL_TX_NOME", OracleType.VarChar);
            param1_in.Direction = ParameterDirection.Input;
            param1_in.Value = infoTipoPlataforma.Nome;
            cmd.Parameters.Add(param1_in);

            OracleParameter param2_in = new OracleParameter("P_TIPL_TX_DESCRICAO", OracleType.VarChar);
            param2_in.Direction = ParameterDirection.Input;
            param2_in.Value = infoTipoPlataforma.Descricao;
            cmd.Parameters.Add(param2_in);

            OracleParameter param3_in = new OracleParameter("P_TIPL_TX_LAMINA", OracleType.VarChar);
            param3_in.Direction = ParameterDirection.Input;
            param3_in.Value = infoTipoPlataforma.Lamina;
            cmd.Parameters.Add(param3_in);

            OracleParameter param4_in = new OracleParameter("P_TIPL_IN_PERFURACAO", OracleType.Int32);
            param4_in.Direction = ParameterDirection.Input;
            param4_in.Value = infoTipoPlataforma.Perfuracao;
            cmd.Parameters.Add(param4_in);

            OracleParameter param5_in = new OracleParameter("P_TIPL_IN_PRODUCAO", OracleType.Int32);
            param5_in.Direction = ParameterDirection.Input;
            param5_in.Value = infoTipoPlataforma.Producao;
            cmd.Parameters.Add(param5_in);

            OracleParameter param6_in = new OracleParameter("P_TIPL_IN_CONTROLE_POCOS", OracleType.Int32);
            param6_in.Direction = ParameterDirection.Input;
            param6_in.Value = infoTipoPlataforma.ControlePocos;
            cmd.Parameters.Add(param6_in);

            OracleParameter param7_in = new OracleParameter("P_TIPL_TX_ESCOAMENTO", OracleType.VarChar);
            param7_in.Direction = ParameterDirection.Input;
            param7_in.Value = infoTipoPlataforma.Escoamento;
            cmd.Parameters.Add(param7_in);

            OracleParameter param8_in = new OracleParameter("P_TIPL_TX_VANTAGEM", OracleType.VarChar);
            param8_in.Direction = ParameterDirection.Input;
            param8_in.Value = infoTipoPlataforma.Vantagem;
            cmd.Parameters.Add(param8_in);

            OracleParameter param9_in = new OracleParameter("P_TIPL_TX_IMAGEM", OracleType.VarChar);
            param9_in.Direction = ParameterDirection.Input;
            param9_in.Value = infoTipoPlataforma.Imagem;
            cmd.Parameters.Add(param9_in);

            // Definindo o parâmetro de saída da stored procedure.
            OracleParameter output = cmd.Parameters.Add("P_RESULT", OracleType.Int32);
            output.Direction = ParameterDirection.ReturnValue;

            cmd.ExecuteNonQuery();

            conn.Close();

            // Retornando o indicativo se a operação foi executada com sucesso ou não.
            return output.ToString();
        }

        public string Editar(TipoPlataforma infoTipoPlataforma)
        {
            // Obtendo a string de conexão do arquivo appsettings.json e conectando-se ao banco de dados.
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json");
            var config = builder.Build();

            var connectionStringOracle = config.GetConnectionString("Oracle");
            OracleConnection conn = new OracleConnection(connectionStringOracle);
            conn.Open();

            // Definindo a stored procedure para editar o registro.
            OracleCommand cmd = new OracleCommand("PKG_TIPO_PLATAFORMA.SP_EDITAR_TIPO_PLATAFORMA", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // Definindo os parâmetros de entrada da stored procedure.

            OracleParameter param0_in = new OracleParameter("P_TIPL_CD_CODIGO", OracleType.Int32);
            param0_in.Direction = ParameterDirection.Input;
            param0_in.Value = infoTipoPlataforma.Codigo;
            cmd.Parameters.Add(param0_in);

            OracleParameter param1_in = new OracleParameter("P_TIPL_TX_NOME", OracleType.VarChar);
            param1_in.Direction = ParameterDirection.Input;
            param1_in.Value = infoTipoPlataforma.Nome;
            cmd.Parameters.Add(param1_in);

            OracleParameter param2_in = new OracleParameter("P_TIPL_TX_DESCRICAO", OracleType.VarChar);
            param2_in.Direction = ParameterDirection.Input;
            param2_in.Value = infoTipoPlataforma.Descricao;
            cmd.Parameters.Add(param2_in);

            OracleParameter param3_in = new OracleParameter("P_TIPL_TX_LAMINA", OracleType.VarChar);
            param3_in.Direction = ParameterDirection.Input;
            param3_in.Value = infoTipoPlataforma.Lamina;
            cmd.Parameters.Add(param3_in);

            OracleParameter param4_in = new OracleParameter("P_TIPL_IN_PERFURACAO", OracleType.Int32);
            param4_in.Direction = ParameterDirection.Input;
            param4_in.Value = infoTipoPlataforma.Perfuracao;
            cmd.Parameters.Add(param4_in);

            OracleParameter param5_in = new OracleParameter("P_TIPL_IN_PRODUCAO", OracleType.Int32);
            param5_in.Direction = ParameterDirection.Input;
            param5_in.Value = infoTipoPlataforma.Producao;
            cmd.Parameters.Add(param5_in);

            OracleParameter param6_in = new OracleParameter("P_TIPL_IN_CONTROLE_POCOS", OracleType.Int32);
            param6_in.Direction = ParameterDirection.Input;
            param6_in.Value = infoTipoPlataforma.ControlePocos;
            cmd.Parameters.Add(param6_in);

            OracleParameter param7_in = new OracleParameter("P_TIPL_TX_ESCOAMENTO", OracleType.VarChar);
            param7_in.Direction = ParameterDirection.Input;
            param7_in.Value = infoTipoPlataforma.Escoamento;
            cmd.Parameters.Add(param7_in);

            OracleParameter param8_in = new OracleParameter("P_TIPL_TX_VANTAGEM", OracleType.VarChar);
            param8_in.Direction = ParameterDirection.Input;
            param8_in.Value = infoTipoPlataforma.Vantagem;
            cmd.Parameters.Add(param8_in);

            OracleParameter param9_in = new OracleParameter("P_TIPL_TX_IMAGEM", OracleType.VarChar);
            param9_in.Direction = ParameterDirection.Input;
            param9_in.Value = infoTipoPlataforma.Imagem;
            cmd.Parameters.Add(param9_in);

            // Definindo o parâmetro de saída da stored procedure.
            OracleParameter output = cmd.Parameters.Add("P_RESULT", OracleType.Int32);
            output.Direction = ParameterDirection.ReturnValue;

            cmd.ExecuteNonQuery();

            conn.Close();

            // Retornando o indicativo se a operação foi executada com sucesso ou não.
            return output.ToString();
        }

        public void Excluir(int codigo)
        {
            // Obtendo a string de conexão do arquivo appsettings.json e conectando-se ao banco de dados.
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json");
            var config = builder.Build();

            var connectionStringOracle = config.GetConnectionString("Oracle");
            OracleConnection conn = new OracleConnection(connectionStringOracle);
            conn.Open();

            // Definindo a stored procedure para excluir o registro.
            OracleCommand cmd = new OracleCommand("PKG_TIPO_PLATAFORMA.SP_EXCLUIR_TIPO_PLATAFORMA", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // Definindo o parâmetro de entrada da stored procedure.
            OracleParameter param1_in = new OracleParameter("P_TIPL_CD_CODIGO", OracleType.Int32);
            param1_in.Direction = ParameterDirection.Input;
            param1_in.Value = codigo;
            cmd.Parameters.Add(param1_in);

            // Executando o procedimento de exclusão do registro.
            cmd.ExecuteNonQuery();

            conn.Close();
        }

    }
}
