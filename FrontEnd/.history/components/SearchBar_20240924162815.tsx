import useGetDeviceToken from "@/hooks/useGetDeviceToken";
import usePushNotification from "@/hooks/usePushNotification";
import { Ionicons } from "@expo/vector-icons";
import { Text, TouchableOpacity, View, StyleSheet } from "react-native";

const SearchBar: React.FC = () => {
  const { sendPushNotification, notification, showLocalNotification } =
    usePushNotification();
  const getDeviceToken = useGetDeviceToken();
  console.log(getDeviceToken);
  return (
    <View style={styles.searchBar}>
      <View style={styles.searchBarInput}>
        <Ionicons name="search" size={20} color="#080" />
        <Text style={styles.searchBarText}>Quan 9, TPHCM</Text>
      </View>
      <TouchableOpacity
        style={styles.searchBarIcon}
        onPress={() =>
          sendPushNotification(
            "Hello",
            "This is a local notification 123456",
            "ExponentPushToken[dFo0oGBauClzRWPn4gdlHv]"
          )
        }
      >
        <Ionicons name="notifications" size={24} color="#FF951D" />
      </TouchableOpacity>
    </View>
  );
};

const styles = StyleSheet.create({
  searchBar: {
    position: "absolute",
    top: 10,
    flexDirection: "row",
    justifyContent: "space-between",
    alignItems: "center",
    width: "90%",
    paddingHorizontal: 20,
    alignSelf: "center",
    padding: 10,
    shadowColor: "black",
  },
  searchBarInput: {
    backgroundColor: "#fff",
    borderRadius: 20,
    flex: 1,
    flexDirection: "row",
    alignItems: "center",
    paddingHorizontal: 15,
    paddingVertical: 10,
    shadowColor: "#000",
    shadowOffset: { width: 0, height: 4 },
    shadowOpacity: 0.3,
    shadowRadius: 30,
    elevation: 5,
  },
  searchBarText: {
    fontSize: 16,
    marginLeft: 10,
    flex: 1,
  },
  searchBarIcon: {
    padding: 10,
    marginLeft: 15,
  },
});

export default SearchBar;
