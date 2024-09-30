import { StyleSheet } from "react-native";

export const styles = StyleSheet.create({
  container: {
    flex: 1,
    alignItems: "center",
    justifyContent: "center",
    padding: 20,
    backgroundColor: "#fff",
  },
  title: {
    fontSize: 24,
    fontWeight: "bold",
    marginBottom: 30,
    top: -60,
    color: "#F8931E",
  },
  subtitle: {
    fontSize: 16,
    fontWeight: "bold",
    marginBottom: 30,
  },
  codeFieldRoot: {
    marginTop: 20,
    flexDirection: "row",
    justifyContent: "space-between",
  },
  cell: {
    width: 50,
    height: 50,
    lineHeight: 20,
    fontSize: 24,
    borderWidth: 2,
    borderColor: "#00000030",
    textAlign: "center",
    alignItems: "center",
    justifyContent: "center",
    borderRadius: 30,
    padding: 10,
    marginHorizontal: 10,
  },
  focusCell: {
    borderColor: "#F8931E",
  },
  simpleText: {
    color: "#ABABAB",
    textAlign: "center",
    marginBottom: 20,
  },
  verifyButton: {
    backgroundColor: "#F8931E",
    borderRadius: 30,
    padding: 15,
    width: "80%",
    alignItems: "center",
    marginBottom: 20,
  },
  verifyButtonText: {
    fontWeight: "bold",
    color: "#fff",
  },
  resendButton: {
    padding: 15,
  },
  resendButtonText: {
    fontWeight: "bold",
    color: "#F8931E",
  },
  orText: {
    alignSelf: "center",
    fontSize: 16,
    color: "#F8931E",
    marginBottom: 10,
  },
  socialContainer: {
    marginTop: 20,
  },
  socialButtons: {
    flexDirection: "row",
    justifyContent: "space-between",
    marginBottom: 30,
  },
  socialButton: {
    backgroundColor: "#F8931E",
    borderRadius: 10,
    padding: 15,
    width: "48%",
    alignItems: "center",
    marginBottom: 30,
  },
  socialButtonText: {
    color: "#fff",
  },
  signUpButton: {
    borderRadius: 20,
    padding: 15,
    width: "65%",
    alignSelf: "center",
    alignItems: "center",
    marginVertical: 10,
    backgroundColor: "#F8931E",
  },
  signUpButtonText: {
    fontWeight: "bold",
    color: "#fff",
  },
  preSignUpText: {
    fontSize: 16,
    marginBottom: 5,
  },
});