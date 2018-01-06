package recommender;

import static spark.Spark.get;

public class MahoutOutput {

    public static void main(String[] args) {
        get("/", (req, res) -> {
            return "hello from sparkjava.com";
        });
    }
}
