export class BusinessDataModel {
  id: number;
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

export class IdentificationData {
  id: number;
  type: string;
  tva: string;
  emailPro: string;
  description: string;
  logo: string;

}
export class AddressData {
  id: number;
  postalCode: string;
  country: string;
  street: string;
  boxNumber: string;
}




/*

*/
