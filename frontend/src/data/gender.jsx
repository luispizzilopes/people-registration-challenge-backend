export const Gender = {
    Male: 0,
    Female: 1,
    Other: 2,
    PreferNotToSay: 3,
};

export const Genders = [
    { label: 'Masculino', value: Gender.Male },
    { label: 'Feminino', value: Gender.Female },
    { label: 'Outro', value: Gender.Other },
    { label: 'Prefiro não dizer', value: Gender.PreferNotToSay },
];

export const GenderOptions = {
    [Gender.Male]: "Masculino",
    [Gender.Female]: "Feminino",
    [Gender.Other]: "Outro",
    [Gender.PreferNotToSay]: "Prefiro não dizer",
};