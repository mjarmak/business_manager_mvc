import { ImageModel } from "./image";


export class BusinessDataModel {
    id: number;
    identificationData: IdentificationData;
    businessInfo: BusinessInfo;
    workHours: string;
    logo: ImageModel;
    images: ImageModel[];
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
    name: string;
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
