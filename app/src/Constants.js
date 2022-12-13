export const baseUrl = 'https://localhost:7124';

//ref: https://stackoverflow.com/a/55556258/725957
export const currencyFormat = (num) => {
    return '$' + num.toFixed(2).replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,');
};
export const Relationship = {
    0: "None",
    1: "Spouse",
    2: "DomesticPartner",
    3: "Child"
};
export const ReverseRelationship = {
    None : 0,
    Spouse : 1,
    DomesticPartner : 2,
    Child: 3
}
//https://bobbyhadz.com/blog/javascript-format-date-yyyy-mm-dd#:~:text=To%20format%20a%20date%20as%20yyyy-mm-dd%3A%201%20Use,array%20and%20join%20them%20with%20a%20hyphen%20separator.
export const padTo2Digits = (num) => {
    return num.toString().padStart(2, '0');
}
  
export const formatDate = (date) => {
    return [
      date.getFullYear(),
      padTo2Digits(date.getMonth() + 1),
      padTo2Digits(date.getDate()),
    ].join('-');
  }