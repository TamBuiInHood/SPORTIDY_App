import { StyleSheet } from "react-native";

export const styles = StyleSheet.create({
  container: {
    flex: 1,
    alignItems: "center",
    justifyContent: "center",
    backgroundColor: "#ffffff",
    paddingTop: 30
  },
  header: {
    width: "100%",
    padding: 20,
    marginBottom: 40,
    alignItems: "center",
  },
  title: {
    fontSize: 28,
    fontWeight: "bold",
    color: "#F8931E",
  },
  subtitle: {
    fontSize: 16,
    fontWeight: "bold",
    color: "#7e7e7e",
    marginBottom: 20,
  },
  form: {
    width: "100%",
    height: "83%",
    padding: 20,
    borderRadius: 20,
    bottom: 0,
  },
  textInput: {
    color: "#fff",
    marginBottom: 5,
    left: 7,
  },
  inputContainer: {
    width: "100%",
    marginBottom: 20,
  },
  input: {
    backgroundColor: "#fff",
    borderRadius: 17,
    paddingVertical: 15,
    paddingHorizontal: 20,
    marginBottom: 15,
    borderWidth: 1,
    borderColor: "#ddd",
    shadowColor: "#000",
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.1,
    shadowRadius: 5,
    elevation: 3,
    width: "100%",
  },
  signUpButton: {
    borderRadius: 20,
    padding: 15,
    width: "65%",
    alignSelf: "center",
    alignItems: "center",
    marginBottom: 20,
    backgroundColor: "#ffffff",
  },
  signUpButtonText: {
    fontWeight: "bold",
    color: "#F8931E",
  },
  simpleText: {
    color: "#fff",
    textAlign: "center",
    marginBottom: 20,
  },
  googleButton: {
    backgroundColor: "#fff",
    borderRadius: 20,
    padding: 15,
    width: "65%",
    alignSelf: "center",
    alignItems: "center",
    borderWidth: 1,
    borderColor: "#ddd",
    marginVertical: 20,
  },
  googleButtonText: {
    color: "black",
  },
  loginLink: {
    color: "#fff",
    fontWeight: "bold",
  },
});