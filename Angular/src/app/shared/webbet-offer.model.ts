
export class WebbetOffer {
    id:number;
    pair:string;
    type1:number;
    typeX:number;
    type2:number;
    type1X:number;
    typeX2:number;
    type12:number;
    isPartOfTopOffer:boolean;
    sportId: number;


    constructor(id,pair,type1,typeX,type2,type1X,typeX2,type12,isPartOfTopOffer,sportId){
        this.id = id;
        this.pair = pair;
        this.type1 = type1;
        this.typeX = typeX;
        this.type2 = type2;
        this.type1X = type1X;
        this.typeX2 = typeX2;
        this.type12 = type12;
        this.isPartOfTopOffer = isPartOfTopOffer;
        this.sportId = sportId
    }
}