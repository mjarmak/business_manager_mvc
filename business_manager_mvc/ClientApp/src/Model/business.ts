import { ImageModel } from "./image";


export class BusinessDataModel {
    id: number;
    identification: IdentificationData = new IdentificationData();
    businessInfo: BusinessInfo = new BusinessInfo();
    workHours: string;
}

export class BusinessInfo {
    id: number;
    address: AddressData = new AddressData();
    phone : string;
    emailBusiness : string;
    urlSite : string;
    urlInstagram : string;
    urlFaceBook : string;
    urlLinkedIn: string;
    //photos: File[];
}

export class IdentificationData {
    id: number;
    name: string;
    type: string;
    tva: string;
    emailPro: string;
    description: string;
    //logo: File;
}
export class AddressData {
    id: number;
    postalCode: string;
    country: string;
    street: string;
    boxNumber: string;
}
