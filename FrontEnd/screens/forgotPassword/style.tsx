import { StyleSheet } from "react-native";

export const styles = StyleSheet.create({
  container: {
    flex: 1,
    alignItems: "center",
    justifyContent: "center",
    padding: 20,
  },
  header: {
    width: "100%",
    padding: 20,
    marginBottom: 40,
    alignItems: "center",
  },
  title: {
    fontSize: 28,
    position: "relative",
    fontWeight: "bold",
    color: "#F8931E",
    top: -100,
    marginBottom: 20,
  },
  subtitle: {
    fontSize: 16,
    fontWeight: "bold",
    marginBottom: 30,
  },
  inputContainer: {
    width: "100%",
  },
  input: {
    borderColor: "#F8931E",
    borderRadius: 25,
    padding: 15,
    backgroundColor: "#F5F5F5",
    borderWidth: 1,
  },
  resetButton: {
    backgroundColor: "#F8931E",
    borderRadius: 25,
    padding: 15,
    width: "100%",
    alignItems: "center",
    marginVertical: 20,
    marginBottom: 40,
  },
  resetButtonText: {
    color: "#fff",
  },
  backToSignInButton: {
    padding: 15,
  },
  backToSignInButtonText: {
    color: "#000",
  },
  orText: {
    fontSize: 16,
    marginBottom: 10,
    color: "#F8931E",
  },
  signUpButton: {
    backgroundColor: "#F8931E",
    borderRadius: 25,
    padding: 15,
    width: "65%",
    alignSelf: "center",
    alignItems: "center",
    marginVertical: 20,
    marginBottom: 40,
  },
  signUpButtonText: {
    color: "#fff",
  },
});