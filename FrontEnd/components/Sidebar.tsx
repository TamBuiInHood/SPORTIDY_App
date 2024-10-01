import React from 'react';
import { View, Text, TouchableOpacity, StyleSheet } from 'react-native';

const Sidebar = () => {
  return (
    <View style={styles.container}>
      
      <TouchableOpacity style={styles.option}>
        <Text style={styles.optionText}>View history booking fields</Text>
      </TouchableOpacity>
      
      <TouchableOpacity style={styles.option}>
        <Text style={styles.optionText}>Feedback</Text>
      </TouchableOpacity>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    backgroundColor: '#B0E0E6', // Light blue background
    padding: 16,
    borderRadius: 10,
    width: '80%',  // adjust based on screen size
  },
  option: {
    backgroundColor: '#fff',
    padding: 16,
    marginBottom: 10,
    borderRadius: 10,
    borderColor: '#ccc',
    borderWidth: 1,
  },
  optionText: {
    fontSize: 16,
    color: '#0000EE', // blue text with underline
    textDecorationLine: 'underline',
  },
});

export default Sidebar;
