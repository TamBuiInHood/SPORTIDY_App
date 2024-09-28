import React, { useState } from "react";
import {
  View,
  Text,
  TextInput,
  TouchableOpacity,
  StyleSheet,
  Alert,
} from "react-native";

const NewPasswordScreen = () => {
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");

  const handlePasswordChange = () => {
    if (password === confirmPassword) {
      Alert.alert("Success", "Password updated successfully!");
    } else {
      Alert.alert("Error", "Passwords do not match!");
    }
  };

  return (
    <View style={styles.container}>
      <Text style={styles.title}>New Password</Text>

      <Text style={styles.label}>Enter New Password</Text>
      <TextInput
        style={styles.input}
        secureTextEntry={true}
        value={password}
        onChangeText={setPassword}
      />

      <Text style={styles.label}>Confirm Password</Text>
      <TextInput
        style={styles.input}
        secureTextEntry={true}
        value={confirmPassword}
        onChangeText={setConfirmPassword}
      />

      <TouchableOpacity
        style={styles.sendButton}
        onPress={handlePasswordChange}
      >
        <Text style={styles.sendButtonText}>Send</Text>
      </TouchableOpacity>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    top: -90,
    flex: 1,
    backgroundColor: "#FFF",
    padding: 20,
    justifyContent: "center",
  },
  title: {
    top: -30,
    fontSize: 24,
    fontWeight: "bold",
    color: "#F8931E",
    textAlign: "center",
    marginBottom: 30,
  },
  label: {
    fontSize: 16,
    marginBottom: 20,
    color: "#333",
  },
  input: {
    height: 50,
    borderColor: "#F8931E",
    borderWidth: 1,
    borderRadius: 30,
    paddingHorizontal: 20,
    marginBottom: 20,
  },
  sendButton: {
    height: 50,
    width: "65%",
    backgroundColor: "orange",
    borderRadius: 30,
    justifyContent: "center",
    alignItems: "center",
    alignSelf: "center",
    shadowColor: "#000",
    shadowOpacity: 0.2,
    shadowOffset: { width: 0, height: 4 },
    shadowRadius: 8,
    elevation: 5,
    marginTop: 30,
  },
  sendButtonText: {
    fontSize: 18,
    color: "#fff",
    fontWeight: "bold",
  },
});

export default NewPasswordScreen;