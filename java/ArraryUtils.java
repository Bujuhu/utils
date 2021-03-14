import java.util.Arrays;

public class ArraryUtils {
    //Returns the whole array, except for the last element
    public static <T> T[] init(T[] original) {
        return Arrays.copyOf(original, original.length-1);
    }
    //Returns the last element in the array
    public static <T> T last(T[] original) {
        return original[original.length - 1];
    }
}
