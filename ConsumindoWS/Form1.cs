using System;
using System.IO;
using System.Security.Cryptography.Xml;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using System.Xml;
using System.Security.Cryptography;
using System.Net;
using System.Drawing;

namespace ConsumindoWS
{

    public partial class Form1 : Form
    {
        #region Variaveis Privadas
        private string signatureMethod = @"http://www.w3.org/2001/04/xmldsig-more#rsa-sha256";
        private string digestMethod = @"http://www.w3.org/2001/04/xmlenc#sha256";
        private string aq;
        private string error = "";
        private DateTime errorTime;
        #endregion
        public Form1()
        {
            InitializeComponent();
            ConfLog();
            pbStatus.Visible = false;
        }

        #region Selecionar XML
        private void pbXML_Click(object sender, EventArgs e)
        {
            txtLog.Clear();
            Stream arqxml = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\Reinf\\XML";
            openFileDialog1.Filter = "Arquivos xml (*.xml)|*.xml";
            openFileDialog1.Multiselect = true;
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((arqxml = openFileDialog1.OpenFile()) != null)
                    {
                        aq = openFileDialog1.FileName;
                        txtLog.AppendText("XML selecionado:" + "\n");
                        txtLog.AppendText(aq + "\n");
                        openFileDialog1.ShowReadOnly = true;
                    }
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                    errorTime = DateTime.Now;
                    LogErMsg();
                    MessageBox.Show("Erro: Não foi possível ler o arquivo do disco. Detalhes do erro foi gerado no log C:\\Reinf\\Log");
                }
                finally
                {
                    ((IDisposable)arqxml).Dispose();

                }
            }
        }
        #endregion            

        #region Assinar XML
        public XmlDocument assinarXML(XmlDocument documentoXML, X509Certificate2 certificadoX509, string tagAAssinar, string idAtributoTag)
        {
            // Variáveis utilizadas na assinatura
            XmlNodeList nodeParaAssinatura = null;
            SignedXml signedXml = null;
            Reference reference = null;
            KeyInfo keyInfo = null;
            XmlElement sig = null;
            XmlDocument xmlAssinado = null;
            bool temChavePrivada = false;
            bool eValido = true;


            if (eValido)
            {
                // Verifica se o certificado passado por parâmetro possui chave privada.
                // Se não for possível verificar se o certificado tem ou não a chave privada, significa que
                // a instância do objeto X509Certificate2 está nula.
                try
                {
                    temChavePrivada = certificadoX509.HasPrivateKey;
                }
                catch (Exception ex)
                {
                    // Objeto X509Certificate2 passado por parâmetro está nulo
                    MessageBox.Show("Objeto X509Certificate2 passado por parâmetro não foi carregado." + ex.Message);
                }
                if (temChavePrivada)
                {
                    if (!tagAAssinar.Equals(string.Empty))
                    {
                        if (!idAtributoTag.Equals(string.Empty))
                        {
                            try
                            {
                                // Informando qual a tag será assinada
                                nodeParaAssinatura = documentoXML.GetElementsByTagName(tagAAssinar);
                                signedXml = new SignedXml((XmlElement)nodeParaAssinatura[0]);
                                signedXml.SignedInfo.SignatureMethod = signatureMethod;

                                RSACryptoServiceProvider privateKey = (RSACryptoServiceProvider)certificadoX509.PrivateKey;

                                if (!privateKey.CspKeyContainerInfo.HardwareDevice)
                                {
                                    CspKeyContainerInfo enhCsp = new RSACryptoServiceProvider().CspKeyContainerInfo;
                                    CspParameters cspparams = new CspParameters(enhCsp.ProviderType, enhCsp.ProviderName, privateKey.CspKeyContainerInfo.KeyContainerName);
                                    if (privateKey.CspKeyContainerInfo.MachineKeyStore)
                                    {
                                        cspparams.Flags |= CspProviderFlags.UseMachineKeyStore;
                                    }
                                    privateKey = new RSACryptoServiceProvider(cspparams);
                                }

                                // Adicionando a chave privada para assinar o documento
                                signedXml.SigningKey = privateKey;

                                // Referenciando o identificador da tag que será assinada
                                reference = new Reference("#" + nodeParaAssinatura[0].Attributes[idAtributoTag].Value);
                                reference.AddTransform(new XmlDsigEnvelopedSignatureTransform(false));
                                reference.AddTransform(new XmlDsigC14NTransform(false));
                                reference.DigestMethod = digestMethod;

                                // Adicionando a referencia de qual tag será assinada
                                signedXml.AddReference(reference);

                                // Adicionando informações do certificado na assinatura
                                keyInfo = new KeyInfo();
                                keyInfo.AddClause(new KeyInfoX509Data(certificadoX509));
                                signedXml.KeyInfo = keyInfo;

                                // Calculando a assinatura
                                signedXml.ComputeSignature();

                                // Adicionando a tag de assinatura ao documento xml
                                sig = signedXml.GetXml();
                                documentoXML.GetElementsByTagName(tagAAssinar)[0].ParentNode.AppendChild(sig);
                                xmlAssinado = new XmlDocument();
                                xmlAssinado.PreserveWhitespace = true;
                                xmlAssinado.LoadXml(documentoXML.OuterXml);
                            }
                            catch (Exception ex)
                            {
                                // Falha ao assinar documento XML
                                error = ex.Message;
                                errorTime = DateTime.Now;
                                LogErMsg();
                                MessageBox.Show("Falha ao assinar documento XML, detalhes do erro foi gerado no log C:'\'Reinf'\'log." + ex.Message);
                            }
                        }
                        else
                        {
                            // String que informa o id da tag XML a ser assinada está vazia
                            error = "String que informa o id da tag XML a ser assinada está vazia";
                            errorTime = DateTime.Now;
                            LogErMsg();
                            MessageBox.Show("String que informa o id da tag XML a ser assinada está vazia");
                        }
                    }
                    else
                    {
                        // String que informa a tag XML a ser assinada está vazia
                        error = "String que informa a tag XML a ser assinada está vazia";
                        errorTime = DateTime.Now;
                        LogErMsg();
                        MessageBox.Show("String que informa a tag XML a ser assinada está vazia");
                    }
                }
                else
                {
                    // Certificado Digital informado não possui chave privada
                    error = "Certificado Digital informado não possui chave privada";
                    errorTime = DateTime.Now;
                    LogErMsg();
                    MessageBox.Show("Certificado Digital informado não possui chave privada");
                }
            }

            return xmlAssinado;
        }

        private string obtemElementoAssinar(XmlDocument arquivo)
        {

            string tipoEvento = null;
            if (arquivo.OuterXml.Contains("evtInfoContri")) tipoEvento = "evtInfoContri";
            else if (arquivo.OuterXml.Contains("evtServTom")) tipoEvento = "evtServTom";
            else if (arquivo.OuterXml.Contains("evtReabreEvPer")) tipoEvento = "evtReabreEvPer";
            else if (arquivo.OuterXml.Contains("evtFechaEvPer")) tipoEvento = "evtFechaEvPer";
            else if (arquivo.OuterXml.Contains("evtExclusao")) tipoEvento = "evtExclusao";/*
            else if (arquivo.OuterXml.Contains("evtFechamentoeFinanceira")) tipoEvento = "evtFechamentoeFinanceira";
            else if (arquivo.OuterXml.Contains("evtMovOpFin")) tipoEvento = "evtMovOpFin";
            else if (arquivo.OuterXml.Contains("evtMovPP")) tipoEvento = "evtMovPP";
            */
            return tipoEvento;
        }
        private XmlDocument AssinarEventosDoArquivo(X509Certificate2 certificadoAssinatura)
        {

            XmlDocument arquivoXML = new XmlDocument();
            try
            {
                arquivoXML.Load(aq);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possivel carregar XML indicado : " + ex.Message);
                return null;
            }

            XmlNamespaceManager nsmgr = new XmlNamespaceManager(arquivoXML.NameTable);
            nsmgr.AddNamespace("Reinf", arquivoXML.DocumentElement.NamespaceURI);

            XmlNodeList eventos = arquivoXML.SelectNodes("//Reinf:loteEventos/Reinf:evento", nsmgr);


            if (eventos.Count <= 0)
            {
                MessageBox.Show("Não encontrou eventos no arquivo de lotes selecionado");
                return null;
            }

            // Assina cada evento do arquivo            
            foreach (XmlNode node in eventos)
            {
                XmlDocument xmlDocEvento = new XmlDocument();
                xmlDocEvento.LoadXml(node.InnerXml);

                string tipoEvento = obtemElementoAssinar(xmlDocEvento);
                if (tipoEvento != null)
                {
                    XmlDocument xmlDocEventoAssinado = new XmlDocument();
                    xmlDocEventoAssinado = assinarXML(xmlDocEvento, certificadoAssinatura, tipoEvento, "id");
                    if (xmlDocEventoAssinado == null)
                    {
                        break;
                    }
                    node.InnerXml = xmlDocEventoAssinado.InnerXml;

                    //string pathEventoAssinado = txtCaminhoArquivo.Text.Trim().Replace(".xml", "-"+node.Attributes["id"].Value+"-ASSINADO.xml");
                    //xmlDocEventoAssinado.Save(pathEventoAssinado);
                }
                else
                {
                    MessageBox.Show("Processo de assinatura abortado. Existe eventos invalido");
                    break;
                }
            }

            return arquivoXML;
        }
        public X509Certificate2 SelecionarCertificadoAssinaturaArquivo()
        {
            X509Certificate2 vRetorna;
            X509Certificate2 oX509Cert = new X509Certificate2();
            X509Store store = new X509Store("MY", StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
            X509Certificate2Collection collection = (X509Certificate2Collection)store.Certificates;
            X509Certificate2Collection collection1 = (X509Certificate2Collection)collection.Find(X509FindType.FindByTimeValid, DateTime.Now, false);
            X509Certificate2Collection collection2 = (X509Certificate2Collection)collection.Find(X509FindType.FindByKeyUsage, X509KeyUsageFlags.DigitalSignature, false);
            X509Certificate2Collection scollection = X509Certificate2UI.SelectFromCollection(collection2, "Certificado(s) Digital(is) disponível(is)", "Selecione o certificado digital para uso no aplicativo", X509SelectionFlag.SingleSelection);

            if (scollection.Count == 0)
            {
                string msgResultado = "Nenhum certificado digital foi selecionado ou o certificado selecionado está com problemas.";
                MessageBox.Show(msgResultado, "Advertência", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                vRetorna = null;
            }
            else
            {
                oX509Cert = scollection[0];
                vRetorna = oX509Cert;
            }

            return vRetorna;
        }


        private void pbAssinar_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(aq))
            {
                MessageBox.Show("Selecione um arquivo XML assinado.");
                return;
            }
            if (!System.IO.File.Exists(aq))
            {
                MessageBox.Show("Arquivo não existe no disco.");
                return;
            }

            XmlDocument xmlAssinado = new XmlDocument();
            try
            {
                xmlAssinado.Load(aq);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possivel carregar XML indicado : " + ex.Message);
                return;
            }

            if (!VerificarExistenciaAssinatura(xmlAssinado))
            {

                IniAssinar();

            }
            else
            {
                DialogResult result = MessageBox.Show("O documento XML já está assinado, deseja validar ?", "XML Assinado!",
               MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    ValidarAssinatura(xmlAssinado);
                }
                else if (result == DialogResult.No)
                {
                    return;
                }

            }
        }

        #endregion

        #region Validar Assinatura
        private void pbValidar_Click(object sender, EventArgs e)
        {
            IniValidacao();
        }
        private void IniValidacao()
        {
            if (String.IsNullOrEmpty(aq))
            {
                MessageBox.Show("Selecione um arquivo XML assinado.");
                return;
            }
            if (!System.IO.File.Exists(aq))
            {
                MessageBox.Show("Arquivo não existe no disco.");
                return;
            }

            XmlDocument xmlAssinado = new XmlDocument();
            try
            {
                xmlAssinado.Load(aq);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possivel carregar XML indicado : " + ex.Message);
                return;
            }

            if (!VerificarExistenciaAssinatura(xmlAssinado))
            {

                DialogResult result = MessageBox.Show("O documento XML não está assinado, deseja assinar ?", "XML não assinado!",
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    IniAssinar();
                }
                else if (result == DialogResult.No)
                {
                    return;
                }
                else if (result == DialogResult.Cancel)
                {
                    return;
                }

            }

            ValidarAssinatura(xmlAssinado);
        }

        private void IniAssinar()
        {
            if (String.IsNullOrEmpty(aq))
            {
                MessageBox.Show("Selecione um arquivo XML para assinar.");
                return;
            }

            if (!System.IO.File.Exists(aq))
            {
                MessageBox.Show("Arquivo não existe no disco.");
                return;
            }


            // Seleciona o certificado de assinatura e assina o arquivo
            X509Certificate2 cert = SelecionarCertificadoAssinaturaArquivo();
            if (cert == null)
            {
                MessageBox.Show("Certificado não selecionado.");
                return;
            }

            XmlDocument arquivoXMLAssinado = AssinarEventosDoArquivo(cert);

            if (arquivoXMLAssinado != null)
            {
                string pathArquivoAssinado = aq;
                //string pathArquivoAssinado = aq.Replace(".xml", "-ASSINADO.xml");
                // arquivoXMLAssinado.Save(pathArquivoAssinado);
                using (XmlTextWriter xmlWriter = new XmlTextWriter(pathArquivoAssinado, null))
                {
                    xmlWriter.Formatting = Formatting.None;
                    arquivoXMLAssinado.Save(xmlWriter);
                }
                MessageBox.Show("Arquivo " + pathArquivoAssinado + " assinado com sucesso!", "Arquivo assinado!", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                string destino = aq.Replace(".xml", "-NAO-ASSINADO.xml");
                File.Move(aq, destino);
                MessageBox.Show("Arquivo em " + destino);
                txtLog.Clear();

            }
        }

        private void ValidarAssinatura(XmlDocument xmlAssinado)
        {
            try
            {
                // Selecionar as tags de assinatura.
                XmlNodeList nodeList = xmlAssinado.GetElementsByTagName("Signature", "*");

                // Verifica se o xml do evento foi assinado.
                if (nodeList.Count <= 0)
                {
                    MessageBox.Show("O evento não esta assinado.");
                    return;
                }

                bool assinaturasValidas = true;
                foreach (XmlNode assinatura in nodeList)
                {
                    XmlDocument evento = new XmlDocument() { PreserveWhitespace = true };
                    evento.LoadXml(assinatura.ParentNode.OuterXml);

                    SignedXml signedXml = new SignedXml(evento);
                    signedXml.LoadXml((XmlElement)assinatura);

                    // Carregar certificado do Xml num objeto X509Certificate2
                    string pubKey = signedXml.KeyInfo.GetXml().InnerText;
                    byte[] pubKeyBytes = Convert.FromBase64String(pubKey);
                    X509Certificate2 x509 = new X509Certificate2(pubKeyBytes);

                    // Verifica se a assinatura é rsa-sha256 
                    if (!signedXml.SignatureMethod.Equals("http://www.w3.org/2001/04/xmldsig-more#rsa-sha256"))
                    {
                        MessageBox.Show("O documento  " + ((Reference)signedXml.SignedInfo.References[0]).Uri + " não possui uma assinatura digital rsa-sha256.");
                    }
                    else
                    {
                        // Se a assinatura XML e o certificado digital são válidos
                        if (!signedXml.CheckSignature(x509, false))
                        {
                            // Se somente a assinatura é válida
                            if (signedXml.CheckSignature(x509, true))
                            {
                                // Assinatura é válida e o certificado é inválido.
                                MessageBox.Show("Certificado Digital X509 extraído do documento XML é inválido " + ((Reference)signedXml.SignedInfo.References[0]).Uri);
                            }
                            else
                            {
                                MessageBox.Show("Assinatura Digital do documento XML é inválida " + ((Reference)signedXml.SignedInfo.References[0]).Uri);
                            }
                            assinaturasValidas = false;
                        }
                    }
                }

                if (assinaturasValidas)
                {
                    MessageBox.Show("A assinatura e certificado do(s) evento(s) é/estão válido(s).");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Falha ao verificar a assinatura do documento XML", ex.Message);
            }
        }

        private bool VerificarExistenciaAssinatura(XmlDocument xml)
        {
            // Criando o objeto de assinatura passando como parâmetro o documento que será verificado
            SignedXml signedXml = new SignedXml(xml);

            // Selecionar as tags de assinatura
            XmlNodeList nodeList = xml.GetElementsByTagName("Signature", "*");

            // Carregar a tag de assinatura
            return nodeList[0] != null;
        }
        #endregion

        #region Configuração do LOG
        public void ConfLog()
        {
            txtLog.Multiline = true;
            txtLog.ScrollBars = ScrollBars.Vertical;
            txtLog.AcceptsReturn = true;
            txtLog.AcceptsTab = true;
            txtLog.WordWrap = true;
            txtLog.Text = "Bem Vindo!";
        }


        //define error information method
        private void LogErMsg()
        {
            string path = @"C:\Reinf\Log\log.txt";

            bool ok = Directory.Exists(@"C:\Reinf\Log");
            if (!ok)
                Directory.CreateDirectory(@"C:\Reinf\Log");
            if (!File.Exists(path))
            {
                StreamWriter sw = File.CreateText(path);
                sw.WriteLine($"Error Message:({error}\tErrorTime:{errorTime})");
                sw.Close();
            }
            else
            {
                StreamWriter sw = new StreamWriter(path, true);
                sw.WriteLine($"Error Message:({error}\tErrorTime:{errorTime})");
                sw.Close();
            }

        }
        #endregion

        #region Envio de XML
        private void pbSend_Click(object sender, EventArgs e)
        {
            pbStatus.Visible = true;
            pbStatus.Value = 0;

            #region Envio para DEV
            if (RbDEV.Checked) //dev
            {

                if (aq != null)
                {
                    X509Certificate2 cert = SelecionarCertificadoAssinaturaArquivo();
                    if (cert == null)
                    {
                        MessageBox.Show("Certificado não selecionado.");
                        return;
                    }
                    pbStatus.Value = 5;




                    #region Chamada do Web Service 


                    var ws = new wsReInf.RecepcaoLoteReinf();
                    ws.Timeout = 120000;
                    ServicePointManager.Expect100Continue = false;

                    try
                    {
                        ws.ClientCertificates.Add(cert);
                        pbStatus.Value = 10;
                        XmlTextReader XML = new XmlTextReader(aq);
                        XmlDocument doc = new XmlDocument();
                        doc.Load(aq);
                        XML.MoveToContent();
                        XmlNode cd = doc.ReadNode(XML);
                        pbStatus.Value = 20;


                        var dados = ws.ReceberLoteEventos(cd);
                        try
                        {
                            pbStatus.Value = 30;
                            if (aq != null)
                            {
                                pbStatus.Value = 70;
                                using (var sw = new StringWriter())
                                {
                                    using (var xw = new XmlTextWriter(sw))
                                    {
                                        pbStatus.Value = 80;
                                        xw.Formatting = Formatting.Indented;
                                        xw.Indentation = 2;
                                        dados.WriteTo(xw);
                                    }
                                    string dt, tm, cm;
                                    dt = DateTime.Now.ToShortDateString();
                                    tm = DateTime.Now.ToLongTimeString();
                                    dt = dt.Replace("/", "");
                                    tm = tm.Replace(":", "");
                                    pbStatus.Value = 90;
                                    cm = @"c:\REINF\Retorno\" + dt + tm + ".xml";

                                    bool exists = Directory.Exists(@"c:\REINF\Retorno\");
                                    if (!exists)
                                        txtLog.AppendText("Criando pasta para gravar arquivos de retorno." + "\n");
                                    Directory.CreateDirectory(@"C:\REINF\Retorno\");

                                    File.WriteAllText(cm, sw.ToString());
                                    pbStatus.Value = 100;
                                    txtLog.AppendText("XML de Retorno gravado em:" + "\n");
                                    txtLog.AppendText(cm + "\n");
                                    MessageBox.Show("O xml foi enviado com sucesso, por favor aguarde o status de retorno! ", "XML Enviado.", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    XmlTextReader x = new XmlTextReader(cm);

                                    while (x.Read())
                                    {
                                        if (x.NodeType == XmlNodeType.Element && x.Name == "dscResp" || x.NodeType == XmlNodeType.Element && x.Name == "descricao")
                                            txtLog.AppendText(x.ReadString() + "\n");
                                        pbStatus.Value = 0;
                                    }
                                    x.Close();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Por favor selecione o arquivo XML!");
                            }
                        }

                        catch (Exception ex)
                        {
                            error = ex.Message;
                            errorTime = DateTime.Now;
                            //LogErMsg();                       
                            pbStatus.Value = 0;
                        }

                    }
                    catch (Exception ex)
                    {
                        string erro = ex.ToString();
                        if (erro.Contains("404:"))
                        {
                            error = ex.Message;
                            errorTime = DateTime.Now;
                            LogErMsg();
                            MessageBox.Show("Erro no servidor! \n Servidor do REINF indisponivel, por favor tente mais tarde! \n Log com detalhes gravado em C:\\Reinf\\log", "Server Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            pbStatus.Value = 0;
                            pbStatus.Visible = false;
                            txtLog.Clear();
                        }
                        else if (erro.Contains("403:"))
                        {
                            error = ex.Message;
                            errorTime = DateTime.Now;
                            LogErMsg();
                            MessageBox.Show("Acesso negado! \n Verifique se o certificado selecionado é valido.", "Atenção!!! Problema com o Certificado.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            pbStatus.Value = 0;
                            pbStatus.Visible = false;
                            txtLog.Clear();
                        }
                        else
                            error = ex.Message;
                        errorTime = DateTime.Now;
                        LogErMsg();
                        MessageBox.Show("Erro no servidor! \n Servidor do REINF indisponivel, por favor tente mais tarde! \n Log com detalhes gravado em C:\\Reinf\\log", "Server Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        pbStatus.Value = 0;
                    }

                    #endregion


                }
                else
                {
                    pbStatus.Visible = false;
                    MessageBox.Show("Por favor selecione o arquivo XML!");
                }
            }
            #endregion

            #region Envio para Prod
            else if (RbProd.Checked)
            {
                if (aq != null)
                {
                    X509Certificate2 cert = SelecionarCertificadoAssinaturaArquivo();
                    if (cert == null)
                    {
                        MessageBox.Show("Certificado não selecionado.");
                        return;
                    }
                    pbStatus.Value = 5;




                    #region Chamada do Web Service 
                    var ws = new WsReinfProd.RecepcaoLoteReinf();
                    ws.Timeout = (120000 * 3);
                    ServicePointManager.Expect100Continue = false;

                    try
                    {
                        ws.ClientCertificates.Add(cert);
                        pbStatus.Value = 10;
                        XmlTextReader XML = new XmlTextReader(aq);
                        XmlDocument doc = new XmlDocument();
                        doc.Load(aq);
                        XML.MoveToContent();
                        XmlNode cd = doc.ReadNode(XML);
                        pbStatus.Value = 20;


                        var dados = ws.ReceberLoteEventos(cd);
                        try
                        {
                            pbStatus.Value = 30;
                            if (aq != null)
                            {
                                pbStatus.Value = 70;
                                using (var sw = new StringWriter())
                                {
                                    using (var xw = new XmlTextWriter(sw))
                                    {
                                        pbStatus.Value = 80;
                                        xw.Formatting = Formatting.Indented;
                                        xw.Indentation = 2;
                                        dados.WriteTo(xw);
                                    }
                                    string dt, tm, cm;
                                    dt = DateTime.Now.ToShortDateString();
                                    tm = DateTime.Now.ToLongTimeString();
                                    dt = dt.Replace("/", "");
                                    tm = tm.Replace(":", "");
                                    pbStatus.Value = 90;
                                    cm = @"c:\REINF\Retorno\" + dt + tm + ".xml";

                                    bool exists = Directory.Exists(@"c:\REINF\Retorno\");
                                    if (!exists)
                                        txtLog.AppendText("Criando pasta para gravar arquivos de retorno." + "\n");
                                    Directory.CreateDirectory(@"C:\REINF\Retorno\");

                                    File.WriteAllText(cm, sw.ToString());
                                    pbStatus.Value = 100;
                                    txtLog.AppendText("XML de Retorno gravado em:" + "\n");
                                    txtLog.AppendText(cm + "\n");
                                    MessageBox.Show("O xml foi enviado com sucesso, por favor aguarde o status de retorno! ", "XML Enviado.", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    XmlTextReader x = new XmlTextReader(cm);

                                    while (x.Read())
                                    {
                                        if (x.NodeType == XmlNodeType.Element && x.Name == "dscResp" || x.NodeType == XmlNodeType.Element && x.Name == "descricao")
                                            txtLog.AppendText(x.ReadString() + "\n");
                                        pbStatus.Value = 0;
                                    }
                                    x.Close();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Por favor selecione o arquivo XML!");
                            }
                        }

                        catch (Exception ex)
                        {
                            error = ex.Message;
                            errorTime = DateTime.Now;
                            //LogErMsg();                       
                            pbStatus.Value = 0;
                        }

                    }
                    catch (Exception ex)
                    {
                        string erro = ex.ToString();
                        if (erro.Contains("404:"))
                        {
                            error = ex.Message;
                            errorTime = DateTime.Now;
                            LogErMsg();
                            MessageBox.Show("Erro no servidor! \n Servidor do REINF indisponivel, por favor tente mais tarde! \n Log com detalhes gravado em C:\\Reinf\\log", "Server Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            pbStatus.Value = 0;
                            pbStatus.Visible = false;
                            txtLog.Clear();
                        }
                        else if (erro.Contains("403:"))
                        {
                            error = ex.Message;
                            errorTime = DateTime.Now;
                            LogErMsg();
                            MessageBox.Show("Acesso negado! \n Verifique se o certificado selecionado é valido.", "Atenção!!! Problema com o Certificado.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            pbStatus.Value = 0;
                            pbStatus.Visible = false;
                            txtLog.Clear();

                        }
                        else
                            error = ex.Message;
                        errorTime = DateTime.Now;
                        LogErMsg();
                        MessageBox.Show("Erro no servidor! \n Servidor do REINF indisponivel, por favor tente mais tarde! \n Log com detalhes gravado em C:\\Reinf\\log", "Server Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        pbStatus.Value = 0;
                    }

                    #endregion


                }
                else
                {
                    pbStatus.Visible = false;
                    MessageBox.Show("Por favor selecione o arquivo XML!");
                }
            }
            #endregion

            else
            {
                MessageBox.Show("Por favor selecione o Servidor para envio.");
                RbDEV.ForeColor = Color.Red;
                RbProd.ForeColor = Color.Red;
                return;
            }


        }
        #endregion

        #region Consulta Lotes
        private void ConsultaLote(string nrInsc, string nrRecibo)
        {
            if (nrRecibo != null && nrRecibo != null)
            {
                X509Certificate2 cert = SelecionarCertificadoAssinaturaArquivo();
                if (cert == null)
                {
                    MessageBox.Show("Certificado não selecionado.");
                    return;
                }
                pbStatus.Value = 5;
                #region Chamada do Web Service de Consulta 

                var ws = new WSConsultaReinfProd.ConsultasReinf();
                ws.Timeout = 120000;
                ServicePointManager.Expect100Continue = false;

                try
                {
                    ws.ClientCertificates.Add(cert);    
                    var dados = ws.ConsultaInformacoesConsolidadas(1, nrInsc, nrRecibo);

                    try
                    {
                        using (var sw = new StringWriter())
                        {
                            using (var xw = new XmlTextWriter(sw))
                            {                               
                                xw.Formatting = Formatting.Indented;
                                xw.Indentation = 2;
                                dados.WriteTo(xw);
                            }
                            string dt, tm, cm;
                            dt = DateTime.Now.ToShortDateString();
                            tm = DateTime.Now.ToLongTimeString();
                            dt = dt.Replace("/", "");
                            tm = tm.Replace(":", "");
                            txtLog.Clear();
                            cm = @"c:\REINF\Retorno Consulta\" + dt + tm + ".xml";

                            bool exists = Directory.Exists(@"c:\REINF\Retorno Consulta\");
                            if (!exists)
                                txtLog.AppendText("Criando pasta para gravar arquivos de retorno." + "\n");
                            Directory.CreateDirectory(@"C:\REINF\Retorno Consulta\");

                            File.WriteAllText(cm, sw.ToString());
                            pbStatus.Value = 100;
                            txtLog.AppendText("XML de Retorno gravado em:" + "\n");
                            txtLog.AppendText(cm + "\n");
                            MessageBox.Show("Consulta realizada com sucesso, por favor aguarde o status de retorno! ", "Consulta Realizada.", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            XmlTextReader x = new XmlTextReader(cm);

                            while (x.Read())
                            {
                                if (x.NodeType == XmlNodeType.Element && x.Name == "dscResp" || x.NodeType == XmlNodeType.Element && x.Name == "descricao")
                                    txtLog.AppendText(x.ReadString() + "\n");
                                pbStatus.Value = 0;
                            }
                            x.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        error = ex.Message;
                        errorTime = DateTime.Now;
                        pbStatus.Value = 0;
                    }
                }
                catch (Exception ex)
                {
                    #region tratativa de erros
                    string erro = ex.ToString();
                    if (erro.Contains("404:"))
                    {
                        error = ex.Message;
                        errorTime = DateTime.Now;
                        LogErMsg();
                        MessageBox.Show("Erro no servidor! \n Servidor do REINF indisponivel, por favor tente mais tarde! \n Log com detalhes gravado em C:\\Reinf\\log", "Server Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        pbStatus.Value = 0;
                        pbStatus.Visible = false;
                        txtLog.Clear();
                    }
                    else if (erro.Contains("403:"))
                    {
                        error = ex.Message;
                        errorTime = DateTime.Now;
                        LogErMsg();
                        MessageBox.Show("Acesso negado! \n Verifique se o certificado selecionado é valido.", "Atenção!!! Problema com o Certificado.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        pbStatus.Value = 0;
                        pbStatus.Visible = false;
                        txtLog.Clear();

                    }
                    else
                        error = ex.Message;
                    errorTime = DateTime.Now;
                    LogErMsg();
                    MessageBox.Show("Erro no servidor! \n Servidor do REINF indisponivel, por favor tente mais tarde! \n Log com detalhes gravado em C:\\Reinf\\log", "Server Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    pbStatus.Value = 0;
                    #endregion
                }

                #endregion
            }
        }
        #endregion

        private void RbDEV_CheckedChanged(object sender, EventArgs e)
        {
            if (RbDEV.Checked)
            {
                RbDEV.ForeColor = Color.Black;
                RbProd.ForeColor = Color.Black;
                RbProd.Checked = false;
            }
            else if (RbProd.Checked)
            {
                RbDEV.Checked = false;
                RbDEV.ForeColor = Color.Black;
                RbProd.ForeColor = Color.Black;
            }
        }

        private void pnConsultar_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btEnviar_Click(object sender, EventArgs e)
        {
            ConsultaLote(txtNrInsc.Text, txtNrRecibo.Text);
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            pnConsultar.Show();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            pnConsultar.Hide();
            txtNrInsc.Clear();
            txtNrRecibo.Clear();                
                
        }

        private void txtNrInsc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }

}


