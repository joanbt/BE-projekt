package recommender;

import static spark.Spark.get;

import java.sql.*;

public class MahoutOutput {

    public static void main(String[] args) {
        get("/", (req, res) -> {
//            String id = req.params(":id");
            String response = "";
            try {
                Connection conn = DriverManager.getConnection("jdbc:mysql://mysql:3306/ikea_db","admin", "admin1234");
                Statement stmt = conn.createStatement();

                ResultSet rs = stmt.executeQuery("SELECT sku,customer_id,title " +
                        "FROM catalog_product_entity join review on catalog_product_entity.entity_id=review.entity_pk_value " +
                        "join review_detail on review.review_id=review_detail.review_id WHERE review_detail.customer_id=6");
                while ( rs.next() ) {

                    response += rs.getString("sku") + " "
                            + rs.getString("customer_id") + " "
                            + rs.getString("title")+ "\n";

                }
                conn.close();
            } catch (SQLException e) {
                e.printStackTrace();
            }
            return "Opinie 6: " + response;
        });
    }
}
