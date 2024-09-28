import { StyleSheet } from "react-native";

export const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: "#f8f8f8",
    padding: 16,
  },
  headerLeftIconsContainer: {
    top: 25,
    position: "absolute",
    flexDirection: "row",
    justifyContent: "space-between",
    alignSelf: "flex-start",
    zIndex: 1,
    backgroundColor: "transparent",
    marginBottom: 30,
  },
  headerRightIconsContainer: {
    top: 20,
    position: "absolute",
    flexDirection: "row",
    justifyContent: "space-between",
    alignSelf: "flex-end",
    zIndex: 1,
    backgroundColor: "transparent",
    marginBottom: 30,
  },
  headerRightIcon: {
    margin: 5,
  },
  profileHeader: {
    top: 30,
    alignItems: "center",
    marginBottom: 20,
  },
  profileImage: {
    width: 90,
    height: 90,
    borderRadius: 50,
    marginBottom: 10,
    borderWidth: 1,
  },
  name: {
    fontSize: 24,
    fontWeight: "bold",
    color: "#000",
  },
  genderAge: {
    fontSize: 16,
    color: "#777",
    marginBottom: 10,
  },
  description: {
    fontSize: 16,
    color: "#000",
    paddingHorizontal: 20,
    marginBottom: 40,
  },
  section: {
    marginBottom: 20,
  },
  sectionHeader: {
    flexDirection: "row",
    justifyContent: "space-between",
    alignItems: "center",
    marginBottom: 10,
  },
  sectionTitle: {
    fontSize: 18,
    fontWeight: "bold",
    color: "#333",
  },
  addLink: {
    fontSize: 14,
    color: "#f9a825",
  },
  sportCard: {
    backgroundColor: "#fff",
    padding: 10,
    borderRadius: 10,
    flexDirection: "row",
    justifyContent: "space-between",
    alignItems: "center",
    shadowColor: "#000",
    shadowOpacity: 0.1,
    shadowRadius: 5,
    elevation: 3,
  },
  sportInfo: {
    flexDirection: "row",
    alignItems: "center",
  },
  sportIcon: {
    width: 40,
    height: 40,
    marginRight: 10,
  },
  sportName: {
    fontSize: 16,
    fontWeight: "bold",
  },
  sportDetails: {
    alignItems: "flex-end",
  },
  ratingContainer: {
    flexDirection: "row",
  },
  starIcon: {
    width: 20,
    height: 20,
    marginLeft: 2,
  },
  position: {
    fontSize: 14,
    color: "#777",
  },
  clubCard: {
    backgroundColor: "#fff",
    padding: 10,
    borderRadius: 10,
    flexDirection: "row",
    alignItems: "center",
    shadowColor: "#000",
    shadowOpacity: 0.1,
    shadowRadius: 5,
    elevation: 3,
  },
  clubLogo: {
    width: 40,
    height: 40,
    marginRight: 10,
  },
  clubInfo: {
    flexDirection: "column",
  },
  clubName: {
    fontSize: 16,
    fontWeight: "bold",
  },
  clubDetails: {
    fontSize: 14,
    color: "#777",
  },
  linkText: {
    fontSize: 14,
    color: "#42a5f5",
    marginVertical: 10,
    textAlign: "center",
  },
  feedbackLink: {
    fontSize: 14,
    color: "#e53935",
    marginVertical: 10,
    textAlign: "center",
  },
});