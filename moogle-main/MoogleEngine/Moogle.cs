namespace MoogleEngine;


public static class Moogle
{ 
   public static SearchItem[] items=new SearchItem[4];
    public static SearchResult Query(string query) {
   Consulta.Modificar(query);
  Consulta.Importancia(query);
   Consulta.DocsResultantes(query);
   Consulta.Snippet(query);
           if(Consulta.show()){
              items[0]=new SearchItem("No se encontraron resultados","Sin resultado:(",0);
         items[1]=new SearchItem("Sin resultado:(", "Sin resultado:(", 0);
         items[2]=new SearchItem("Sin resultado:(", "Sin resultado:(", 0);
         items[3]=new SearchItem("Sin resultado:(", "Sin resultado:(", 0);
             
               } else if(Consulta.score[1]==0){
       items[0]=new SearchItem(Consulta.Docs[0], Consulta.snippet[0], Consulta.score[0]);
         items[1]=new SearchItem("Sin otro resultado relevante:(", "Sin otro resultado relevante:(", 0);
         items[2]=new SearchItem("Sin otro resultado relevante:(", "Sin otro resultado relevante:(", 0);
         items[3]=new SearchItem("Sin otro resultado relevante:(", "Sin otro resultado relevante:(", 0);
}else if (Consulta.score[2]==0){
    items[0]=new SearchItem(Consulta.Docs[0], Consulta.snippet[0], Consulta.score[0]);
    items[1]=new SearchItem(Consulta.Docs[1], Consulta.snippet[1], Consulta.score[1]);
    items[2]=new SearchItem("Sin otro resultado relevante:(", "Sin otro resultado relevante:(", 0);
    items[3]=new SearchItem("Sin otro resultado relevante:(", "Sin otro resultado relevante:(", 0);
     
    
  } else if(Consulta.score[3]==0){
      items[0]= new SearchItem(Consulta.Docs[0], Consulta.snippet[0], Consulta.score[0]);
    items[1]=new SearchItem(Consulta.Docs[1], Consulta.snippet[1], Consulta.score[1]);
    items[2] =new SearchItem(Consulta.Docs[2], Consulta.snippet[2], Consulta.score[2]);
    items[3]=new SearchItem("Sin otro resultado relevante:(", "Sin otro resultado relevante:(", 0);
  } else{
      items[0]= new SearchItem(Consulta.Docs[0], Consulta.snippet[0], Consulta.score[0]);
    items[1]=new SearchItem(Consulta.Docs[1], Consulta.snippet[1], Consulta.score[1]);
    items[2] =new SearchItem(Consulta.Docs[2], Consulta.snippet[2], Consulta.score[2]);
    items[3]=new SearchItem( Consulta.Docs[3], Consulta.snippet[3], Consulta.score[3]);
  }
     
        return new SearchResult(items, query);
    }
  
    
}
