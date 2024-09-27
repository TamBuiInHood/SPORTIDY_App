import React, { useState } from 'react';
import { View, Text, StyleSheet, ScrollView, TouchableOpacity, Modal } from 'react-native';
import CommentModal from './comment';

interface Post {
  author: string;
  date: string;
  content: string;
}

const Discussion: React.FC = () => {
  const [posts] = useState<Post[]>([
    {
      author: 'Tanas',
      date: '08/05/2024 6:30 PM',
      content: 'Emotion After playing: So interesting !!!\nThis is the first time I feel good after playing one kind of sport. The coach and teammate also support me a lot. Hope I have it in the next time.',
    },
    {
      author: 'Yen Hoang',
      date: '08/06/2024 6:35 PM',
      content: 'Emotion After playing: So interesting !!!\nTeam bên kia cần tập luyện nhiều hơn nhé',
    },
    {
      author: 'Tanas',
      date: '08/05/2024 6:30 PM',
      content: 'Emotion After playing: So interesting !!!\nThis is the first time I feel good after playing one kind of sport. The coach and teammate also support me a lot. Hope I have it in the next time.',
    },
    {
      author: 'Tanas',
      date: '08/05/2024 6:30 PM',
      content: 'Emotion After playing: So interesting !!!\nThis is the first time I feel good after playing one kind',
    },
  ]);

  const [modalVisible, setModalVisible] = useState<boolean>(false); // State for modal visibility

  return (
    <View style={styles.container}>
      <Text style={styles.title}>Forum</Text>
      <ScrollView style={styles.scroll}>
        {posts.map((post, index) => (
          <View key={index} style={styles.post}>
            <View style={styles.postHeader}>
              <Text style={styles.author}>{post.author}</Text>
              <Text style={styles.date}>{post.date}</Text>
            </View>
            <Text style={styles.content}>{post.content}</Text>
          </View>
        ))}
      </ScrollView>

      <TouchableOpacity
        style={styles.chatBubble}
        onPress={() => setModalVisible(true)} // Show modal
      >
        <Text style={styles.chatBubbleText}>💬</Text>
      </TouchableOpacity>

      <Modal
        animationType="slide"
        transparent={true}
        visible={modalVisible}
        onRequestClose={() => setModalVisible(false)} // Handle back button
      >
        <View style={styles.modalBackground}>
          <CommentModal onClose={() => setModalVisible(false)} />
        </View>
      </Modal>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#fff',
    padding: 16,
  },
  title: {
    backgroundColor: '#FFD966',
    color: '#F8A933',
    fontSize: 18,
    fontWeight: 'bold',
    textAlign: 'center',
    borderWidth: 1,
    borderColor: '#F8A933',
  },
  scroll: {
    flex: 1,
  },
  post: {
    backgroundColor: '#f5f5f5',
    padding: 16,
    marginBottom: 16,
    borderRadius: 8,
  },
  postHeader: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    marginBottom: 8,
  },
  author: {
    fontWeight: 'bold',
  },
  date: {
    color: '#999',
  },
  content: {
    fontSize: 16,
  },
  chatBubble: {
    position: 'absolute',
    bottom: 30,
    right: 20,
    backgroundColor: '#FF4500',
    width: 60,
    height: 60,
    borderRadius: 30,
    justifyContent: 'center',
    alignItems: 'center',
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.8,
    shadowRadius: 2,
    elevation: 5,
  },
  chatBubbleText: {
    fontSize: 28,
    color: '#fff',
  },
  modalBackground: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
    backgroundColor: 'rgba(0, 0, 0, 0.5)', // Semi-transparent background
  },
});

export default Discussion;
