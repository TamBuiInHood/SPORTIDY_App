import { StyleSheet } from "react-native";

export const styles = StyleSheet.create({
    card: {
      flexDirection: 'row',
      marginVertical: 10,
      backgroundColor: '#fff',
      borderRadius: 10,
      overflow: 'hidden',
      shadowColor: '#000',
      shadowOffset: { width: 0, height: 1 },
      shadowOpacity: 0.3,
      shadowRadius: 2,
      elevation: 3,
    },
    image: {
      width: 100,
      height: 100,
    },
    infoContainer: {
      flex: 1,
      padding: 10,
    },
    name: {
      fontSize: 16,
      fontWeight: 'bold',
    },
    locationRow: {
      flexDirection: 'row',
      alignItems: 'center',
      marginVertical: 5,
    },
    location: {
      marginLeft: 5,
      color: '#888',
    },
    priceRow: {
      flexDirection: 'row',
      alignItems: 'center',
    },
    price: {
      marginLeft: 5,
      color: '#888',
    },
    viewMore: {
      marginTop: 10,
      color: '#48C9B0',
    },
  });