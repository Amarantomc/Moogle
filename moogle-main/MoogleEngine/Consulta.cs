namespace MoogleEngine;
 public static class Consulta{
public static string [] busqueda;
public static string [] aux =new string[Matriz.ListDiccionariosTF.Count];
public static string [] auxII =new string[Matriz.ListDiccionariosTF.Count];

    public static float [] idf;
    public static float[] score =new float[Matriz.ListDiccionariosTF.Count]; 
   public static Dictionary<float,string> Diccionariotfidf=new Dictionary<float,string>();
    public static string[] Docs=new string[4];
    public static string[] snippet=new string[4];
   
   public static void Modificar(string query){
        
        query=Load.EliminarTildes(query);
        query=Load.EliminarCaracteres(query);
        busqueda=new string[query.Length];
        busqueda=query.Split(" ",StringSplitOptions.RemoveEmptyEntries);
         }
      private static void SearchIdf(){
        
        float count=0;
       for(int i=0;i<busqueda.Length;i++){
        for(int j=0;j<Matriz.ListDiccionarios.Count;j++){
            if(Matriz.ListDiccionarios[j].ContainsKey(busqueda[i])){
               count++;
            }
        }   //*= xq para los caracteres especiales inicio idf
            idf[i]*=Matriz.Idf((float)Matriz.ListDiccionarios.Count,(float)count);
            count=0;
             }
               }
      private static void TfIdf (){
       SearchIdf();
        float count=0;
         for(int i=0;i<Matriz.ListDiccionariosTF.Count;i++){
          aux[i]="No existe";
          auxII[i]="No existe";

              for(int j=0;j<idf.Length;j++){
               if(Matriz.ListDiccionariosTF[i].ContainsKey(busqueda[j])){
                  count+=Matriz.ListDiccionariosTF[i][busqueda[j]]*idf[j];
               } 
              }
                score[i]=count;
                if(Diccionariotfidf.ContainsKey(count)){  
                  Diccionariotfidf[count]+="~~~"+Load.GetTitle()[i];
               
                } else {Diccionariotfidf.Add(count,Load.GetTitle()[i]);  }
            count=0;
              }
      }  
    
       public static void DocsResultantes(string query){
         TfIdf();
          string [] result;
        result=query.Split(" ",StringSplitOptions.RemoveEmptyEntries);
        for(int i=0;i<result.Length;i++){
          if(result[i].Contains("!")){
         Delete(query);
          } else if(result[i].Contains("^")){
            Must(query);
          }
        }
         Array.Sort(score);
        Array.Reverse(score); 
        for(int i=0;i<Docs.Length;i++){ 
          Docs[i]=null;
          string test="";
          int count =0;
          bool countII=false;
          for(int k=0;k<aux.Length;k++){
          if(!Diccionariotfidf[score[i]].Contains(aux[k])){
         count ++;
          }  if(Diccionariotfidf[score[i]].Contains(auxII[k])){
            countII=true;
          } 
          } if(count==aux.Length && score[i]!=0){
            if(Diccionariotfidf[score[i]].Contains("~~~")){
            test=Diccionariotfidf[score[i]];
              for(int j=0;j<test.Length;j++){
                  if(test[j]=='~'){
                    test=test.Substring(0,j);
                    break;
                  }
              } 
              Docs[i]=test;
              test="";
            }else Docs[i]=Diccionariotfidf[score[i]]; 
            
          }   if(countII && score[i]!=0){
             if(Diccionariotfidf[score[i]].Contains("~~~")){
            test=Diccionariotfidf[score[i]];
              for(int j=0;j<test.Length;j++){
                  if(test[j]=='~'){
                    test=test.Substring(0,j);
                    break;
                  }
              } 
              Docs[i]=test;
            
            }else Docs[i]=Diccionariotfidf[score[i]]; 
            
          } if(Docs[i]==null){
            score[i]=0;
            Docs[i]="No existe";
          }
           }
      } 
       public static bool show(){
        if(score[0]==0){
          return true;
        } 
        return false;
       }
       public static void Importancia(string query){
        idf=new float[busqueda.Length];
      
        string[]result;
        result=query.Split(" ",StringSplitOptions.RemoveEmptyEntries);
        for(int i=0;i<result.Length;i++){
          idf[i]=(float)0.5;
        if(result[i].Contains("*")){
          int count=0;
          for(int j=0;j<result[i].Length;j++){
            
          if(result[i][j].Equals('*')){
              if(count>=2){
                count+=1;
              }else count+=2;
            } else break;
          } idf[i]=idf[i]*count;
            } 
       } 
       } 
       public static void Snippet(string query){
       query=query.ToLower();
        string [] Query= new string[query.Length];
        Query=query.Split(" ",StringSplitOptions.RemoveEmptyEntries);
        string texto="";
        for(int l=0;l<Query.Length;l++){
          if(Query[l].Contains("*")||Query[l].Contains("!")||Query[l].Contains("^")){
            for(int r=0;r<Query[l].Length;r++){
              if(!Query[l][r].Equals('*')&&!Query[l][r].Equals('!')&&!Query[l][r].Equals('^')){
                 Query[l]=Query[l].Substring(r,Query[l].Length-r);
                 break;
              }
            }
          }
        }
       float a = idf.Max();
       int posicion = Array.IndexOf(idf,a);
          for(int i=0;i<Docs.Length;i++){
            for(int j=0;j<Load.GetPath().Length;j++){
              if(Load.GetPath()[j].Contains(Docs[i])){
                texto=File.ReadAllText(Load.GetPath()[j]);
                texto=texto.ToLower();
                string []Text= new string [texto.Length];
                Text=texto.Split(" ");
                for(int k=0; k<texto.Length/10;k++){
                     if(Text[k].Equals(Query[posicion])){
                      texto= string.Join(" ",Text , k ,50);
                      break;
                     }
                }if(texto.Length>350){
                  texto=texto.Substring(0,300);
                } snippet[i]=texto;
                break;
              } 
            }  
          }
       } 
       public static void Delete(string query){
        string [] result;
        result=query.Split(" ",StringSplitOptions.RemoveEmptyEntries);
        for(int i=0;i<result.Length;i++){
          if(result[i].Contains("!")){
            for(int j=0;j<Matriz.ListDiccionariosTF.Count;j++){
              string test=result[i].Substring(1,result[i].Length-1);
                if(Matriz.ListDiccionariosTF[j].ContainsKey(test)){
                  aux[j]=Load.GetTitle()[j];
                  
                 }else aux[j]="No existe";
               }
               }
        }
       } 
       public static void Must(string query){
        string [] result;
        result=query.Split(" ",StringSplitOptions.RemoveEmptyEntries);
               for(int i=0;i<result.Length;i++){
          if(result[i].Contains("^")){
            for(int j=0;j<Matriz.ListDiccionariosTF.Count;j++){
              string test=result[i].Substring(1,result[i].Length-1);
                if(Matriz.ListDiccionariosTF[j].ContainsKey(test)){
                  auxII[j]=Load.GetTitle()[j];
                  
                 }else auxII[j]="No existe";
               }
               }
        }
        
       }
    

 }
  