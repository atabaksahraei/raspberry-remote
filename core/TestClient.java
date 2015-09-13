import java.io.*;
import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.io.OutputStream;
import java.io.PrintStream;
import java.net.Socket;
import java.io.BufferedReader;

public class TestClient {
    public static void main(String[] arrstring) {
        try {
            Socket socket = new Socket("192.168.2.114", 11337);
            DataOutputStream dataOutputStream = new DataOutputStream(socket.getOutputStream());
            dataOutputStream.write(arrstring[0].getBytes());
            dataOutputStream.flush();

            InputStreamReader inputReader = new InputStreamReader(socket.getInputStream() );

            BufferedReader bufferedReader = new BufferedReader(inputReader);
            String result = bufferedReader.readLine();
            System.out.println(result);

            dataOutputStream.close();
            socket.close();
        }
        catch (Exception var1_2) {
            System.out.println(var1_2);
        }
    }
}