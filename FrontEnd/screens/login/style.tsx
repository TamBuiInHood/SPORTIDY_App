import { StyleSheet } from "react-native";

export const styles = StyleSheet.create({
  container: {
    flex: 1,
    alignItems: "center",
    justifyContent: "center",
    backgroundColor: "#ffffff",
    paddingTop: 20
  },
  logo: {
    resizeMode: "contain",
    alignSelf: "center",
    left: 30,
    width: 180,
    height: 180,
    marginBottom: 40,
  },
  form: {
    width: "100%",
    height: "75%",
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
  forgotPassword: {
    alignSelf: "flex-end",
  },
  forgotPasswordText: {
    color: "#fff",
  },
  loginButton: {
    backgroundColor: "#FFF",
    borderRadius: 20,
    padding: 15,
    width: "65%",
    alignSelf: "center",
    alignItems: "center",
    marginBottom: 20,
  },
  loginButtonText: {
    fontWeight: "bold",
    color: "#F74c06",
  },
  orText: {
    marginBottom: 20,
  },
  googleButton: {
    backgroundColor: "#FFF",
    borderRadius: 20,
    padding: 15,
    width: "65%",
    alignSelf: "center",
    flexDirection: "row",
    alignItems: "center",
    justifyContent: "center",
    marginVertical: 20,
  },
  googleIcon: {
    width: 24,
    height: 24,
    marginRight: 10,
  },
  googleButtonText: {
    color: "#000",
  },
  simpleText: {
    color: "#fff",
    textAlign: "center",
    marginBottom: 20,
  },
  signUpLink: {
    color: "#fff",
    fontWeight: "bold",
  },
});