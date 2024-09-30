import React from 'react';
import { View, Text, TextInput, TouchableOpacity, StyleSheet } from 'react-native';

interface CommentModalProps {
    onClose: () => void;
  }

const CommentModal: React.FC<CommentModalProps> = ({ onClose }) => {
  return (
    <View style={styles.modalContainer}>
      <Text style={styles.title}>Comment</Text>

      <TextInput
        style={styles.input}
        placeholder="Title (optional)"
      />

      <TextInput
        style={[styles.input, styles.commentInput]}
        placeholder="Write comment"
        multiline
      />

      <TouchableOpacity style={styles.button} onPress={onClose}>
        <Text style={styles.buttonText}>Post</Text>
      </TouchableOpacity>
    </View>
  );
};

const styles = StyleSheet.create({
  modalContainer: {
    flex: 1,
    backgroundColor: '#FFF7E1',  
    padding: 80,
    margin: 50
  },
  title: {
    backgroundColor: '#FFD966',
    color: '#F8A933',
    fontSize: 18,
    fontWeight: 'bold',
    textAlign: 'center',
    paddingVertical: 40,
    paddingHorizontal: 40,
    borderRadius: 20,
    alignSelf: 'center',
    marginBottom: 20,
  },
  input: {
    borderWidth: 1,
    borderColor: '#E0E0E0',
    borderRadius: 10,
    paddingHorizontal: 10,
    paddingVertical: 15,
    fontSize: 16,
    backgroundColor: '#fff',
    marginBottom: 15,
  },
  commentInput: {
    height: 150, 
    textAlignVertical: 'top', 
  },
  button: {
    backgroundColor: '#F8A933',
    borderRadius: 30,
    paddingVertical: 15,
    alignItems: 'center',
    justifyContent: 'center',
    marginTop: 20,
  },
  buttonText: {
    color: '#fff',
    fontSize: 18,
    fontWeight: 'bold',
  },
});

export default CommentModal;
