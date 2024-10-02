import api from '@/config/api';
import { Comment } from '@/types/types';
import React, { useState, useEffect } from 'react';
import { View, Text, StyleSheet, ScrollView, TextInput, Button } from 'react-native';

const Discussion: React.FC = () => {
  const [comments, setComments] = useState<Comment[]>([]);
  const [input, setInput] = useState<string>('');
  const [ws, setWs] = useState<WebSocket | null>(null);
  
  // Lấy meetingId từ localStorage
  const meetingId = localStorage.getItem('meetingId');
  
  useEffect(() => {
    if (meetingId) {
      const fetchComments = async () => {
        try {
          const response = await api.getComments(parseInt(meetingId)); // Chuyển đổi sang số
          const result: Comment = response.data;
          setComments(result.list);
        } catch (error) {
          console.error('Error fetching comments:', error);
        }
      };

      fetchComments();

      // Kết nối WebSocket
      const socket = new WebSocket(`wss://localhost:7102/api/sportidywebsocket`);
      setWs(socket);

      // Nhận bình luận mới từ WebSocket
      socket.onmessage = (event) => {
        const newComment = JSON.parse(event.data);
        setComments((prevComments) => [...prevComments, newComment]);
      };

      // Đóng kết nối khi component bị hủy
      return () => {
        socket.close();
      };
    }
  }, [meetingId]);

  // Gửi bình luận mới
  const sendComment = async () => {
    if (input.trim() && ws && ws.readyState === WebSocket.OPEN && meetingId) {
      const userId = 1; // Lấy userId từ trạng thái người dùng hoặc props nếu cần
      const image = ''; // Thêm image nếu cần
      try {
        await api.createComment(userId, input, image, meetingId);
        setInput(''); // Xóa ô nhập sau khi gửi
      } catch (error) {
        console.error('Error creating comment:', error);
      }
    }
  };

  return (
    <View style={styles.container}>
      <Text style={styles.title}>Discussion</Text>
      <ScrollView style={styles.scroll}>
        {comments.map((comment, index) => (
          <View key={index} style={styles.comment}>
            <Text style={styles.userId}>User ID: {comment.userId}</Text>
            <Text style={styles.content}>{comment.content}</Text>
          </View>
        ))}
      </ScrollView>
      <View style={styles.inputContainer}>
        <TextInput
          style={styles.input}
          value={input}
          onChangeText={setInput}
          placeholder="Thêm bình luận..."
        />
        <Button title="Gửi" onPress={sendComment} />
      </View>
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
    fontSize: 18,
    fontWeight: 'bold',
    textAlign: 'center',
    marginBottom: 16,
  },
  scroll: {
    flex: 1,
  },
  comment: {
    backgroundColor: '#f5f5f5',
    padding: 10,
    marginVertical: 5,
    borderRadius: 5,
  },
  userId: {
    fontWeight: 'bold',
  },
  content: {
    marginTop: 5,
  },
  inputContainer: {
    flexDirection: 'row',
    alignItems: 'center',
    marginTop: 10,
  },
  input: {
    flex: 1,
    borderWidth: 1,
    borderColor: '#ccc',
    borderRadius: 5,
    padding: 5,
    marginRight: 5,
  },
});

export default Discussion;
