import React, { useState } from "react";
import { View, Text, TextInput, TouchableOpacity } from "react-native";
import { styles } from "./styles";
import { useNavigation } from "expo-router";
import { NativeStackNavigationProp } from "@react-navigation/native-stack";
import { RootStackParamList } from "../../types/types";
import Divider from "@/components/Divider";
import { LinearGradient } from "expo-linear-gradient";

type SignUpScreenNavigationProp = NativeStackNavigationProp<
  RootStackParamList,
  "SignUp"
>;

const SignUpScreen = () => {
  const navigation = useNavigation<SignUpScreenNavigationProp>();
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");
  const [showPassword, setShowPassword] = useState(false);
  const [showConfirmPassword, setShowConfirmPassword] = useState(false);

  const handleSignUp = () => {
    console.log("Sign up with:", email, password);
  };

  const handleGoogleSignUp = () => {
    console.log("Sign up with Google");
  };

  return (
    <View style={styles.container}>
      <View style={styles.header}>
        <Text style={styles.title}>Create Account</Text>
        <Text style={styles.subtitle}>Sign Up to get started</Text>
      </View>
      <LinearGradient
        colors={[ "#F9BC2C","#F74c06"]}
        style={styles.form}
      >
        <View style={styles.inputContainer}>
          <View>
            <Text style={styles.textInput}>Email Address</Text>
            <TextInput
              style={styles.input}
              value={email}
              onChangeText={setEmail}
              keyboardType="email-address"
              autoCapitalize="none"
            />
          </View>
          <View>
            <Text style={styles.textInput}>Password</Text>
            <TextInput
              style={styles.input}
              value={password}
              onChangeText={setPassword}
              secureTextEntry={!showPassword}
            />
          </View>
          <View>
            <Text style={styles.textInput}>Confirm Password</Text>
            <TextInput
              style={styles.input}
              value={confirmPassword}
              onChangeText={setConfirmPassword}
              secureTextEntry={!showConfirmPassword}
            />
          </View>
        </View>

        <TouchableOpacity style={styles.signUpButton} onPress={handleSignUp}>
          <Text style={styles.signUpButtonText}>Sign up</Text>
        </TouchableOpacity>

        <Divider text="Or Sign Up with" />

        <TouchableOpacity
          style={styles.googleButton}
          onPress={handleGoogleSignUp}
        >
          <Text style={styles.googleButtonText}>Google</Text>
        </TouchableOpacity>

        <TouchableOpacity onPress={() => navigation.navigate("Login")}>
          <Text style={styles.simpleText}>
            Already have an account? <Text style={styles.loginLink}>Login</Text>
          </Text>
        </TouchableOpacity>
        <Divider />
      </LinearGradient>
    </View>
  );
};

export default SignUpScreen;