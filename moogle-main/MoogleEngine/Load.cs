namespace MoogleEngine;

using System.Text.RegularExpressions;

 public static class Load{
     private static  string [] path=Directory.GetFiles(Path.Join("..","Content",""));
     private static  string[] title= new string[path.Length];
      private static  string [][] text=new string[title.Length][];
  
       //Cargar base de datos
       public static void run (){
        string texto="";
        for(int i=0;i<title.Length;i++){
            title[i]=Path.GetFileNameWithoutExtension(path[i]);
            texto=File.ReadAllText(path[i]);
           texto= EliminarTildes(texto);
            texto=EliminarCaracteres(texto);
             text[i]=texto.Split(" ", StringSplitOptions.RemoveEmptyEntries);
              texto="";
            }
               }  
        public static string EliminarTildes( string texto){ //Quitar tildes
        texto = texto.ToLower();
        texto.Replace('á','a');
        texto.Replace('é','e');
        texto.Replace('í','i');
        texto.Replace('ó','o');
        texto.Replace('ú','u');
        return texto;
    } 
        public static string EliminarCaracteres(string texto){ //Dejar solo letras y numeros
          texto=Regex.Replace(texto,@"[^ña-z0-9]"," ");
         return texto;
         }
         public static string[][] GetText(){
          return text;
         }
         public static string[] GetTitle(){
          return title;
         }
         public static string [] GetPath(){
            return path;
         }
       
       

    }