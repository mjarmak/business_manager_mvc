export class BusinessDataModel {
  id: string;
  bdentificationData: IdentificationData;
  businessInfo: BusinessInfo;
  worlHours: string;
}

export class BusinessInfo {
  id: number;
  address: AddressData;
  phone : string;
  emailBusiness : string;
  urlSite : string;
  urlInstagram : string;
  urlFaceBook : string;
  urlLinkedIn : string;
}

export enum BusinessTypeEnum {
  bar,
  club,
  concert,
  studentCircle
}

export class IdentificationData {
  id: string;
  type: BusinessTypeEnum ;
  tva: string;
  emailPro: string;
  description: string;
  logo: string;

}
export class AddressData {
  id: string;
  postalCode: string;
  country: string;
  street: string;
  boxNumber: string;
}




/*

*/
