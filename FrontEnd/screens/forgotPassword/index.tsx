import React, { useState } from "react";
import { View, Text, TextInput, TouchableOpacity, StyleSheet } from "react-native";
import { useNavigation } from "@react-navigation/native";
import { NativeStackNavigationProp } from "@react-navigation/native-stack";
import { RootStackParamList } from "../../types/types";

type ForgotPasswordScreenNavigationProp = NativeStackNavigationProp<
  RootStackParamList,
  "ForgotPassword"
>;

const ForgotPasswordScreen = () => {
  const [email, setEmail] = useState("");
  const navigation = useNavigation<ForgotPasswordScreenNavigationProp>();

  const handleResetPassword = () => {
    // Implement password reset logic here
    if (!email) {
      alert("Please enter your email address");
      return;
    }
    navigation.navigate("Verification");
    console.log("Reset password for:", email);
  };

  const handleBackToSignIn = () => {
    navigation.navigate("Login");
  };

  const handleSignUp = () => {
    navigation.navigate("SignUp");
  };

  return (
    <View style={styles.container}>
      <View style={styles.header}>
        <Text style={styles.title}>Forgot Password</Text>
        <Text style={styles.subtitle}>Enter Email Address</Text>
      </View>

      <View style={styles.inputContainer}>
        <TextInput
          style={styles.input}
          placeholder="Email address"
          value={email}
          onChangeText={setEmail}
          keyboardType="email-address"
          autoCapitalize="none"
        />
      </View>
      <TouchableOpacity
        style={styles.backToSignInButton}
        onPress={handleBackToSignIn}
      >
        <Text style={styles.backToSignInButtonText}>Back to SignIn</Text>
      </TouchableOpacity>

      <TouchableOpacity
        style={styles.resetButton}
        onPress={handleResetPassword}
      >
        <Text style={styles.resetButtonText}>Send</Text>
      </TouchableOpacity>

      <Text style={styles.orText}>Or</Text>
      <Text>Don't have an account?</Text>
      <TouchableOpacity style={styles.signUpButton} onPress={handleSignUp}>
        <Text style={styles.signUpButtonText}>Sign Up</Text>
      </TouchableOpacity>
    </View>
  );
};
const styles = StyleSheet.create({
  container: {
    flex: 1,
    alignItems: "center",
    justifyContent: "center",
    padding: 20,
    backgroundColor: "#ffff"
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
export default ForgotPasswordScreen;
