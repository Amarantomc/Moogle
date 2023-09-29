using System.Collections.Generic;

namespace MoogleEngine;



    public static class Matriz{
     public static List<Dictionary<String,int>> ListDiccionarios =new List<Dictionary<string,int>>();
     public static List<Dictionary<String,float>> ListDiccionariosTF =new List<Dictionary<string,float>>();
     
         static void Diccionario(){
   
          for(int i=0;i<Load.GetText().Length;i++){// por cada documento creo un diccionario
            Dictionary<String,int> Diccionario=new Dictionary<string, int>();
            for(int j=0;j<Load.GetText()[i].Length;j++){
              if(!Diccionario.ContainsKey(Load.GetText()[i][j])){
                 Diccionario.Add(Load.GetText()[i][j],1);
              } else Diccionario[Load.GetText()[i][j]]++;
              
            }ListDiccionarios.Add(Diccionario);
             } 
        } 
        
        public static void DiccionarioTF(){
          Matriz.Diccionario();
         for(int i=0;i<Load.GetText().Length;i++){
            Dictionary<String,float> Diccionariotf=new Dictionary<string, float>();
              for(int j=0;j<Load.GetText()[i].Length;j++){
 if( !Diccionariotf.ContainsKey(Load.GetText()[i][j])){
                 Diccionariotf.Add(Load.GetText()[i][j],Matriz.Tf(ListDiccionarios[i][Load.GetText()[i][j]],Load.GetText()[i].Length));
                 }
            }
                ListDiccionariosTF.Add(Diccionariotf);
                  }
 

          
        }
  
         public static float Tf (float repeticion,float cantPalabras ){
           return repeticion/ cantPalabras;
         }
         public static float Idf (float TotalDocumentos,float documentos){
          if(documentos!=0){
           return (float)Math.Log10(TotalDocumentos/documentos);
          }
          return 0;
         }
      
      //Metodos Algebraicos
       public static int [,] MultiplicarEscalar(int[,]a,int k){ //Multiplicacion de una matriz por un escalar
        
        for(int i=0;i<a.GetLength(0);i++){
            for (int j=0;j<a.GetLength(1);j++){
                a[i,j]=k*a[i,j];
            }
        } return a;
    }
         public static int[,] Traspuesta(int[,]a){ //Matriz traspuesta
          int[,] resultado= new int[a.GetLength(1),a.GetLength(0)];
        
         for(int i=0;i<a.GetLength(0);i++){
            for (int j=0;j<a.GetLength(1);j++){
                resultado[j,i]=a[i,j];
                }
        }   return resultado;
         }
         public static int[,] Sumar(int[,]a,int[,]b){// Sumar matrices
            //**Condicion necesaria para la suma, que las dos matrices sean del mismo orden (a(m*n),b(m*n));  
        int[,] resultado=new int [a.GetLength(0),a.GetLength(1)]; 
         for(int i=0;i<a.GetLength(0);i++){
            for (int j=0;j<a.GetLength(1);j++){
                resultado[i,j]=a[i,j]+b[i,j];
                }
         } return resultado;
        }   
            public static int[,] Restar(int[,]a,int[,]b){// Restar matrices
            //**Condicion necesaria para la resta, que las dos matrices sean del mismo orden (a(m*n),b(m*n));  
        int[,] resultado=new int [a.GetLength(0),a.GetLength(1)]; 
         for(int i=0;i<a.GetLength(0);i++){
            for (int j=0;j<a.GetLength(1);j++){
                resultado[i,j]=a[i,j]-b[i,j];
                }
         } return resultado;
        } 
          static int[,] Multiplicar(int[,]a,int[,]b){ //Multiplicacion de matrices
        int [,] resultado= new int[a.GetLength(0),b.GetLength(1)];
        if(a.GetLength(1)!=b.GetLength(0)){
            //Condicion necesaria para la multiplicacion de matrices(a(m*n),b(n*p))
          return null;
        } else{
          for(int i=0;i<a.GetLength(0);i++){
            for(int j=0;j<b.GetLength(1);j++){
              int count=0;
              for(int k=0;k<a.GetLength(1);k++){
                count+=a[i,k]*b[k,j];
              }
              resultado[i,j]=count;
            }
          }
        } return resultado;
      }   
 
 
        } 


 