package recommender;

import static spark.Spark.get;

import java.io.*;
import java.nio.file.*;

import java.sql.*;
import java.io.File;
import java.io.IOException;
import java.util.*;

import org.apache.mahout.cf.taste.impl.model.file.FileDataModel;
import org.apache.mahout.cf.taste.model.DataModel;
import org.apache.mahout.cf.taste.impl.recommender.*;
import org.apache.mahout.cf.taste.recommender.*;
import org.apache.mahout.cf.taste.common.*;
import org.apache.mahout.cf.taste.similarity.ItemSimilarity;
import org.apache.mahout.cf.taste.impl.similarity.*;


public class MahoutOutput {

    public static void main(String[] args) {
    get("/", (req, res) -> {
            String client_id = req.params(":id");
            String response = "";
	    String resp="";
resp+="client_id"+client_id;
            try {
                Connection conn = DriverManager.getConnection("jdbc:mysql://mysql:3306/ikea_db","admin", "admin1234");
                Statement stmt = conn.createStatement();

                ResultSet rs = stmt.executeQuery("SELECT customer_id,catalog_product_entity.entity_id,title " +
                        "FROM catalog_product_entity join review on catalog_product_entity.entity_id=review.entity_pk_value " +
                        "join review_detail on review.review_id=review_detail.review_id");
                while ( rs.next() ) {

                    response += rs.getString("customer_id") + ","
                            + rs.getString("entity_id") + ","
                            + rs.getString("title")+ "\n";

                }
		//resp+=response;
		FileWriter fooWriter = new FileWriter("/java/src/main/java/recommender/dataset.csv", false); 
		fooWriter.write(response);
		fooWriter.close();
                conn.close();

	DataModel model = new FileDataModel(new File("/java/src/main/java/recommender/dataset.csv"));
	ItemSimilarity itemSimilarity = new EuclideanDistanceSimilarity (model);
	Recommender recommender = new GenericItemBasedRecommender(model, itemSimilarity);
	List<RecommendedItem> recommendations = recommender.recommend(9, 3);//8->clientid

	if(recommendations.size()==0)
		resp+="EMPTY";
	for(RecommendedItem i:recommendations)	
	{
		resp+=i+"\n";
	}

return resp;
}


catch (IOException e)
{
	return "IO "+e.getMessage();
}
catch(TasteException e1)
{
	return "Taste"+e1.getMessage();
}
catch (SQLException e) 
{
        return "SQL+"+e.getMessage();
}




        });
    }
}
