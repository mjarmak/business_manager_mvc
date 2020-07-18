export class BusinessDataModel {
  id: number;
  identification: IdentificationData = new IdentificationData();
  businessInfo: BusinessInfo = new BusinessInfo();
  workHours: string;
}

export class BusinessInfo {
  id: number;
  address: AddressData = new AddressData();
  phone: string;
  emailBusiness: string;
  urlSite: string;
  urlInstagram: string;
  urlFaceBook: string;
  urlLinkedIn: string;
  photoPath1: string;
  photoPath2: string;
  photoPath3: string;
  photoPath4: string;
  photoPath5: string;
}

export class IdentificationData {
  id: number;
  name: string;
  type: string;
  tva: string;
  emailPro: string;
  description: string;
  logoPath: string;
}
export class AddressData {
  id: number;
  postalCode: string;
  country: string;
  street: string;
  boxNumber: string;
}
