package recommender;

import static spark.Spark.get;

import java.sql.*;

public class MahoutOutput {

    public static void main(String[] args) {
        get("/", (req, res) -> {
            String response = "";
            try {
                Connection conn = DriverManager.getConnection("jdbc:mysql://mysql:3306/ikea_db","admin", "admin1234");
                Statement stmt = conn.createStatement();

                ResultSet rs = stmt.executeQuery("SELECT value FROM catalog_product_entity_varchar WHERE value_id = 7266");
                while ( rs.next() ) {
                    String value = rs.getString("value");
                    response += value;
                }
                conn.close();
            } catch (SQLException e) {
                e.printStackTrace();
            }
            return response;
        });
    }
}
