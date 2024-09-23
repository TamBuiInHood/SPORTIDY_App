import { Ionicons } from "@expo/vector-icons";
import { StyleSheet, TouchableOpacity, View } from "react-native";

const ActionIcons: React.FC<{ onSwipedLeft: (cardIndex: number) => void; onSwipedRight: (cardIndex: number) => void }> = ({ onSwipedLeft, onSwipedRight }) => {
  return (
    <View style={styles.actionIcons}>
      <TouchableOpacity style={styles.dislikeIcon} onPress={() => onSwipedLeft(0)}>
        <Ionicons name="close-circle-outline" size={30} color="red" />
      </TouchableOpacity>
      <TouchableOpacity style={styles.likeIcon} onPress={() => onSwipedRight(0)}>
        <Ionicons name="heart-outline" size={30} color="green" />
      </TouchableOpacity>
    </View>
  );
};

const styles = StyleSheet.create({
  actionIcons: {
    position: 'absolute',
    bottom: 10, 
    right: 5,  
    flexDirection: 'row',
    justifyContent: 'space-evenly',
    width: '100%',
    paddingHorizontal: 20,
    
  },
  dislikeIcon: {
    backgroundColor: '#fff',
    borderRadius: 50,
    padding: 15,
    borderWidth: 2,
    borderColor: 'red',
  },
  likeIcon: {
    backgroundColor: '#fff',
    borderRadius: 50,
    padding: 15,
    borderWidth: 2,
    borderColor: 'green',
  },
});
export default ActionIcons
