package com.example.jroncero.sadmodules;

import android.app.AlertDialog;
import android.content.ContentResolver;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.res.AssetManager;
import android.net.Uri;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Base64;
import android.util.Log;
import android.util.Xml;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.EditText;
import android.widget.Toast;

import org.w3c.dom.Attr;
import org.w3c.dom.Document;
import org.w3c.dom.Element;
import org.w3c.dom.NodeList;
import org.xml.sax.InputSource;
import org.xml.sax.SAXException;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.StringReader;
import java.util.ArrayList;
import java.util.Calendar;


import javax.xml.parsers.DocumentBuilder;
import javax.xml.parsers.DocumentBuilderFactory;
import javax.xml.parsers.ParserConfigurationException;

import net.sf.andpdf.pdfviewer.PdfViewerActivity;

public class MainActivity extends AppCompatActivity {

    int fsize;
    String fname;
    String fext;

    private static Context context;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        MainActivity.context = getApplicationContext();


        Intent intent = getIntent();
        String action = intent.getAction();
        if (action.compareTo(Intent.ACTION_VIEW) == 0) {
            //Toast.makeText(getApplicationContext(), "Primer if", Toast.LENGTH_SHORT).show();
            String scheme = intent.getScheme();
            ContentResolver resolver = getContentResolver();
            if (scheme.compareTo(ContentResolver.SCHEME_FILE) == 0) {
                Uri uri = intent.getData();
                readSadFile(uri.getPath());

            }

        }
    }


    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.menu_main, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();

        //noinspection SimplifiableIfStatement
        if (id == R.id.action_settings) {

            //Toast.makeText(getApplicationContext(), "SAD Alcatel-Lucent 2015", Toast.LENGTH_LONG).show();
            AlertDialog.Builder alert = new AlertDialog.Builder(this);

            alert.setTitle("About");
            alert.setMessage("Aplicación SAD\nAlcatel-Lucent\n2015\nsad@alu.com");

            alert.setPositiveButton("Ok", new DialogInterface.OnClickListener() {
                public void onClick(DialogInterface dialog, int whichButton) {

                }
            });

            alert.show();
            return true;
        }

        return super.onOptionsItemSelected(item);
    }


    public void clickOpenFile(View v){
        abrirFichero();
    }

    private void parseHeaderNode(Element headerElement){
        NodeList paramList = headerElement.getElementsByTagName("PARAM");

        for (int j = 0; j < paramList.getLength(); j++) {
            Element paramElement = (Element) paramList.item(j);

            if (paramElement.hasAttribute("name")) {
                Attr paramNameAttr = paramElement.getAttributeNode("name");

                switch (paramNameAttr.getValue()) {
                    case "file_size":
                        try {
                            fsize = Integer.parseInt(paramElement.getFirstChild().getNodeValue());
                        } catch(NumberFormatException nfe) {
                            fsize = 0;
                        }
                        break;
                }
            }
        }

    }

    private String parseFingerprintNode(Element challElement){
        NodeList paramList = challElement.getElementsByTagName("PARAM");
        Element paramElement = (Element) paramList.item(0);
        String fichero = paramElement.getFirstChild().getNodeValue() + ".txt";

        AssetManager assetManager = getAssets();

        // To load text file
        InputStream input;
        try {
            input = assetManager.open(fichero);
            int size = input.available();
            byte[] buffer = new byte[size];
            input.read(buffer);
            input.close();
            // byte buffer into a string
            String text = new String(buffer);
            return text;
        } catch (IOException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
            return "null";
        }
    }

    private String parseFechaNode(Element challElement){

        NodeList paramList = challElement.getElementsByTagName("PARAM");

        long min = 0;
        long max = 0;
        long mask = 0;

        for (int j = 0; j < paramList.getLength(); j++) {
            Element paramElement = (Element) paramList.item(j);
            if (paramElement.hasAttribute("name")) {
                Attr paramNameAttr = paramElement.getAttributeNode("name");
                switch (paramNameAttr.getValue()) {
                    case "min":
                        min = Long.parseLong(paramElement.getFirstChild().getNodeValue());
                        break;
                    case "max":
                        max = Long.parseLong(paramElement.getFirstChild().getNodeValue());
                        break;
                    case "mask":
                        mask = Long.parseLong(paramElement.getFirstChild().getNodeValue());
                        break;
                }
            }
        }
        String resultado;
        Calendar rightNow = Calendar.getInstance();
        long suma_hoy = (rightNow.get(Calendar.YEAR)-1900)*10000 + rightNow.get(Calendar.MONTH)*100 + rightNow.get(Calendar.DAY_OF_MONTH);
        long parte_comun = suma_hoy >> mask;
        long final_hoy = 0;
        long bitString = Long.parseLong("11111111111111111111111111111111", 2);

        if(mask != 0){
            final_hoy = suma_hoy & (bitString >> (32 - mask));
        }
        if ((final_hoy >= min) && (final_hoy <= max))
        {
            resultado = String.valueOf(parte_comun);
        }
        else{
            resultado = "error";
        }
        return resultado;

    }
    private byte[] parseFileNode(Element fileElement){

        String textBase64;

        if (fileElement.hasAttribute("name")){
            Attr fileNameAttr = fileElement.getAttributeNode("name");
            fname = fileNameAttr.getValue();
        }
        if (fileElement.hasAttribute("type")){
            Attr fileNameAttr = fileElement.getAttributeNode("type");
            fext = fileNameAttr.getValue();
        }
        textBase64 = fileElement.getFirstChild().getNodeValue();
        byte[] data = Base64.decode(textBase64, Base64.DEFAULT);

        return data;
    }

    private void challengesFinished(Document doc, ArrayList<String> resultados){

        //Coger el texto cifrado
        NodeList fileList = doc.getElementsByTagName("CIPHEREDFILE");
        Element fileElement = (Element) fileList.item(0);
        byte[] textoCifrado = parseFileNode(fileElement);

        //Si es de tipo pdf
        if(fext.contains("pdf")){
            //Obtener clave de los resultados
            String keytest = "";
            for(int i = 0; i<resultados.size(); i++){
                keytest += resultados.get(i);
            }
            //Descifrar el texto con la clave y RC4
            byte[] textoDescifrado = SADDecipher(textoCifrado, keytest);

            //Crear el nuevo fichero pdf
            String fullName = fname + "." + fext;
            createFile(fullName, textoDescifrado);

            //Ejecutar el visor con el fichero
            openPdf(fullName);
        } else {
            //Sólo se admiten pdfs
            Toast.makeText(getApplicationContext(), "No se puede abrir este tipo de documento (sólo se admiten pdfs de momento)", Toast.LENGTH_LONG).show();
        }

    }

    public void openPdf(String name){

        String path = context.getFilesDir() + "/" + name;
        try {
            final Intent intent = new Intent(MainActivity.this, PDFController.class);
            intent.putExtra(PdfViewerActivity.EXTRA_PDFFILENAME, path);
            startActivity(intent);
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void executeChallenges(final Document doc, final NodeList challList,final int index, final ArrayList<String> resultados){

            //Se coge el primer nodo de challenge.
            Element challElement = (Element) challList.item(index);

            if (challElement.hasAttribute("name")){
                Attr challengeNameAttr = challElement.getAttributeNode("name");
                String contenido;
                //Switch para saber que tipo de challenge es.
                switch (challengeNameAttr.getValue()){

                    //Si es de tipo pass, muestra un edittext para rellenar con la contraseña
                    case "pass":
                        AlertDialog.Builder alert = new AlertDialog.Builder(this);

                        alert.setTitle("Challenge de password");
                        alert.setMessage("Introduzca la password");

                        final EditText input = new EditText(context);
                        alert.setView(input);

                        alert.setPositiveButton("Ok", new DialogInterface.OnClickListener() {
                            public void onClick(DialogInterface dialog, int whichButton) {

                                // Do something with value!
                                String value = input.getText().toString();
                                resultados.add(value);
                                int contador = index+1;
                                if(contador == challList.getLength()){
                                    challengesFinished(doc, resultados);
                                }else{
                                    executeChallenges(doc, challList, contador, resultados);
                                }


                                //una vez cogida la pass, continuar con el código para abrir fichero
                            }
                        });

                        alert.setNegativeButton("Cancel", new DialogInterface.OnClickListener() {
                            public void onClick(DialogInterface dialog, int whichButton) {
                                // Canceled.
                            }
                        });

                        alert.show();
                        break;
                    //Si es de tipo fecha, se realiza un algoritmo como en Windows para ejecutar este challenge
                    case "fecha":
                        contenido = parseFechaNode(challElement);
                        resultados.add(contenido);
                        int contadorFecha = index+1;
                        if(contadorFecha == challList.getLength()){
                            challengesFinished(doc, resultados);
                        }else{
                            executeChallenges(doc, challList, contadorFecha, resultados);
                        }
                        //Challenge de fecha
                        break;
                    //Si es de fingerprint, se comprueban una serie de variables de un fichero.
                    case "fingerprint":
                        contenido = parseFingerprintNode(challElement);
                        resultados.add(contenido);
                        int contadorFinger = index+1;
                        if(contadorFinger == challList.getLength()){
                            challengesFinished(doc, resultados);
                        }else{
                            executeChallenges(doc, challList, contadorFinger, resultados);
                        }
                        break;
                    //Si no es un challenge compatible
                    default:
                        //No se puede leer el SAD
                        Toast.makeText(getApplicationContext(), "El documento contiene challenges incompatibles en Android", Toast.LENGTH_LONG).show();
                        break;
                }
            }
    }
    private void parseSadXML(Document doc)
    {

        //Coger el tamaño del documento
        NodeList headerlist = doc.getElementsByTagName("SAD_HEADER");
        Element headerElement = (Element) headerlist.item(0);
        parseHeaderNode(headerElement);

        //Coger los nodos de los challenges
        NodeList challList = doc.getElementsByTagName("CHALLENGE");
        final ArrayList<String> resultados = new ArrayList<String>();

        //Ejecuta los distintos challenges
        executeChallenges(doc, challList, 0, resultados);


    }

    private byte[] SADDecipher(byte[] textoCifrado, String key){

        byte[] keyBytes = key.getBytes(); //convert key to byte

        SADRC4 rc4 = new SADRC4(keyBytes);

        byte[] textoBytes = rc4.decrypt(textoCifrado); //decryption
        return textoBytes;

    }


    public FileOutputStream getStream(String path) throws FileNotFoundException {
        return context.openFileOutput(path, Context.MODE_PRIVATE);
    }

    public void createFile(String name, byte[] text){

        FileOutputStream fout = null;
        try {
            fout = getStream(name);
            fout.write(text);
        } catch (FileNotFoundException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        }finally{
            if(fout!=null){
                //closing the output stream
                try {
                    fout.close();
                } catch (IOException e) {
                    e.printStackTrace();
                }
            }

        }

    }

    public void readXml(InputSource in_source){

        Log.v("readxml", "Leyendo xml");
        Document document = null;
        DocumentBuilderFactory factory = DocumentBuilderFactory.newInstance();
        try {
            DocumentBuilder db = factory.newDocumentBuilder();
            //InputSource inputSource = new InputSource(in_s);
            //String test = convertStreamToString(in_s);
            document = db.parse(in_source);
            parseSadXML(document);

        } catch (ParserConfigurationException e) {
            Log.e("Error: ", e.getMessage());
        } catch (SAXException e) {
            Log.e("Error: ", e.getMessage());
        } catch (IOException e) {
            Log.e("Error: ", e.getMessage());
        }


    }


    public void readSadFile(String path){

        File fl = new File(path);
        FileInputStream fin = null;
        try {
            fin = new FileInputStream(fl);

            BufferedReader br  = new BufferedReader(new InputStreamReader(fin));
            StringBuilder sb = new StringBuilder();
            String line;
            while ((line = br.readLine()) != null) {
                sb.append(line.replace("\t", "").replace("\n", ""));
            }
            InputSource inputSource = new InputSource(new StringReader(sb.toString()));
            readXml(inputSource);
            fin.close();
        } catch (Exception e) {
            e.printStackTrace();
        }


    }


    public void abrirFichero(){

        //Crea un FileOpenDialog y espera la llamada de vuelta
        SimpleFileDialog FileOpenDialog =  new SimpleFileDialog(MainActivity.this, "FileOpen",
                new SimpleFileDialog.SimpleFileDialogListener()
                {
                    String m_chosen;
                    @Override
                    public void onChosenDir(String chosenDir)
                    {
                        // Se ejecuta cuando se ha pulsado OK.
                        m_chosen = chosenDir;
                        readSadFile(m_chosen);
                    }
                });

        //You can change the default filename using the public variable "Default_File_Name"
        FileOpenDialog.Default_File_Name = "";
        FileOpenDialog.chooseFile_or_Dir();

        /////////////////////////////////////////////////////////////////////////////////////////////////
    }


}
