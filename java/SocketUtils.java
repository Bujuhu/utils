import java.io.IOException;
import java.net.ServerSocket;
import java.net.Socket;

public class SocketUtils {
    public static void close(Socket socket) {
        if (socket != null && !socket.isClosed()) {
            try {
                socket.close();

            } catch (IOException e) {
                System.err.println("Error while closing server socket: " + e.getMessage());
            }
        }
    }
}
