import { View, Text, StyleSheet, ScrollView } from "react-native";
const CreateCommnent = () => {
  return;
  <View>
    <Text style={styles.author}>ABC</Text>
  </View>;
};
const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: "#fff",
    padding: 16,
  },
  title: {
    fontSize: 24,
    fontWeight: "bold",
    marginBottom: 16,
  },
  scroll: {
    flex: 1,
  },
  post: {
    backgroundColor: "#f5f5f5",
    padding: 16,
    marginBottom: 16,
    borderRadius: 8,
  },
  postHeader: {
    flexDirection: "row",
    justifyContent: "space-between",
    marginBottom: 8,
  },
  author: {
    fontWeight: "bold",
  },
  date: {
    color: "#999",
  },
  content: {
    fontSize: 16,
  },
});
export default CreateCommnent;
