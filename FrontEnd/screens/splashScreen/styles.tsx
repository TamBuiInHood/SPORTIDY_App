import { StyleSheet } from "react-native";

export const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: "center",
    alignItems: "center",
  },
  logo: {
    resizeMode: "contain",
    alignSelf: "center",
    left: 30,
    width: 180,
    height: 180,
    marginBottom: 40,
  },
  tagline: {
    fontSize: 26,
    color: "#FF6F00",
    fontWeight: "bold",
    textAlign: "center",
    marginBottom: 40,
    fontFamily: "Cochin", // Or any playful font you prefer
  },
  button: {
    backgroundColor: "#FFA726",
    paddingVertical: 15,
    paddingHorizontal: 100,
    borderRadius: 30,
    marginBottom: 20,
    shadowColor: "#000",
    shadowOffset: { width: 0, height: 3 },
    shadowOpacity: 0.5,
    shadowRadius: 5,
    elevation: 5, 
  },
  buttonText: {
    color: "#fff",
    fontSize: 18,
    fontWeight: "600",
  },
  loginButton: {
    marginTop: 10,
  },
  loginButtonText: {
    fontSize: 16,
    color: "#FF6F00",
  },
  loginText: {
    fontWeight: "bold",
    color: "#FF6F00",
  },
});