import { Comment } from '@/types/types';
import { format } from 'date-fns';
import React, { useState, useEffect } from 'react';
import { View, Text, StyleSheet, ScrollView, TextInput, Button, Image } from 'react-native';

interface DiscussionProps {
  meetingId: number; // Accept meetingId as a prop
}

const Discussion: React.FC<DiscussionProps> = ({ meetingId }) => {
  const [comments, setComments] = useState<Comment[]>([
    {
      commentId: 1,
      user: ' Tanas',
      content: "Thật là tuyệt vời!",
      commentDate: "2024-10-02T14:56:30.147",
      image: null,
      meetingId: meetingId,
    },
    {
      commentId: 2,
      user: 'YenH',
      content: "Tôi cũng nghĩ như vậy!",
      commentDate: "2024-10-02T14:56:35.903",
      image: null,
      meetingId: meetingId,
    },
    // Thêm nhiều bình luận khác nếu cần
  ]);

  const [input, setInput] = useState<string>('');

  // Hàm gửi bình luận mới
  const sendComment = async () => {
    if (!input.trim()) {
      alert('Vui lòng nhập nội dung bình luận.');
      return;
    }

    const newComment: Comment = {
      commentId: comments.length + 1, // Tạo ID mới cho bình luận
      user: 'Bảo', // Thay thế bằng ID người dùng thực tế
      content: input,
      commentDate: new Date().toISOString(),
      image: null,
      meetingId: meetingId,
    };

    setComments([...comments, newComment]); // Thêm bình luận mới vào danh sách
    setInput(''); // Xóa nội dung bình luận sau khi gửi
  };

  return (
    <View style={styles.container}>
      <Text style={styles.title}>Discussion</Text>
      <ScrollView style={styles.scroll}>
        {comments.map((comment) => (
          <View key={comment.commentId} style={styles.comment}>
            <Text style={styles.userId}>{comment.user}</Text>
            <Text style={styles.content}>{comment.content}</Text>
            <Text style={styles.commentDate}>
              {format(new Date(comment.commentDate), 'dd/MM/yyyy HH:mm:ss')}
            </Text>
            {comment.image && (
              <Image source={{ uri: comment.image }} style={styles.image} />
            )}
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
