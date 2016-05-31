var jms_data = [
    {id: '1', question_2:'2', question_3:'1', question_4:'1', question_5:'3', question_6:'4', question_7:'3', question_8:'3',category: 'M'},
    {id: '2', question_2:'1', question_3:'1', question_4:'0', question_5:'0', question_6:'1', question_7:'2', question_8:'2',category: 'J'},
    {id: '3', question_2:'4', question_3:'1', question_4:'1', question_5:'2', question_6:'4', question_7:'3', question_8:'3',category: 'S'},
    {id: '4', question_2:'3', question_3:'2', question_4:'4', question_5:'3', question_6:'4', question_7:'4', question_8:'3',category: 'M'},
    {id: '5', question_2:'4', question_3:'1', question_4:'1', question_5:'4', question_6:'0', question_7:'3', question_8:'4',category: 'M'},
    {id: '6', question_2:'3', question_3:'1', question_4:'1', question_5:'1', question_6:'2', question_7:'3', question_8:'4',category: 'M'},
    {id: '7', question_2:'0', question_3:'2', question_4:'1', question_5:'2', question_6:'2', question_7:'1', question_8:'3',category: 'J'},
    {id: '8', question_2:'1', question_3:'2', question_4:'2', question_5:'2', question_6:'2', question_7:'4', question_8:'2',category: 'J'},
    {id: '9', question_2:'3', question_3:'2', question_4:'2', question_5:'1', question_6:'4', question_7:'4', question_8:'4',category: 'J'},
    {id: '10', question_2:'3', question_3:'2', question_4:'3', question_5:'4', question_6:'4', question_7:'3', question_8:'4',category: 'M'},
    {id: '11', question_2:'3', question_3:'3', question_4:'1', question_5:'2', question_6:'2', question_7:'1', question_8:'2',category: 'M'},
    {id: '12', question_2:'3', question_3:'2', question_4:'3', question_5:'3', question_6:'2', question_7:'3', question_8:'2',category: 'S'},
    {id: '13', question_2:'2', question_3:'2', question_4:'0', question_5:'4', question_6:'4', question_7:'1', question_8:'3',category: 'J'},
    {id: '14', question_2:'2', question_3:'3', question_4:'3', question_5:'3', question_6:'2', question_7:'3', question_8:'4',category: 'J'},
    {id: '15', question_2:'1', question_3:'2', question_4:'2', question_5:'1', question_6:'2', question_7:'3', question_8:'2',category: 'M'},
    {id: '16', question_2:'4', question_3:'3', question_4:'4', question_5:'3', question_6:'3', question_7:'3', question_8:'4',category: 'S'},
    {id: '17', question_2:'3', question_3:'2', question_4:'3', question_5:'3', question_6:'3', question_7:'4', question_8:'4',category: 'S'},
    {id: '18', question_2:'4', question_3:'3', question_4:'3', question_5:'2', question_6:'3', question_7:'3', question_8:'4',category: 'S'},
    {id: '19', question_2:'0', question_3:'1', question_4:'0', question_5:'0', question_6:'2', question_7:'1', question_8:'1',category: 'J'},
    {id: '20', question_2:'2', question_3:'1', question_4:'1', question_5:'2', question_6:'2', question_7:'3', question_8:'2',category: 'S'},
    {id: '21', question_2:'2', question_3:'2', question_4:'2', question_5:'3', question_6:'3', question_7:'3', question_8:'3',category: 'M'},
    {id: '22', question_2:'2', question_3:'3', question_4:'1', question_5:'2', question_6:'2', question_7:'2', question_8:'3',category: 'M'},
    {id: '23', question_2:'4', question_3:'0', question_4:'4', question_5:'3', question_6:'2', question_7:'0', question_8:'4',category: 'M'},
    {id: '24', question_2:'2', question_3:'1', question_4:'3', question_5:'3', question_6:'3', question_7:'3', question_8:'4',category: 'S'},
    {id: '25', question_2:'1', question_3:'4', question_4:'0', question_5:'1', question_6:'1', question_7:'0', question_8:'2',category: 'J'},
    {id: '26', question_2:'2', question_3:'1', question_4:'2', question_5:'1', question_6:'2', question_7:'2', question_8:'2',category: 'M'},
    {id: '27', question_2:'2', question_3:'3', question_4:'2', question_5:'3', question_6:'2', question_7:'3', question_8:'4',category: 'M'},
    {id: '28', question_2:'2', question_3:'3', question_4:'2', question_5:'2', question_6:'2', question_7:'3', question_8:'3',category: 'M'},
    {id: '29', question_2:'3', question_3:'3', question_4:'4', question_5:'4', question_6:'3', question_7:'4', question_8:'4',category: 'S'},
    {id: '30', question_2:'4', question_3:'3', question_4:'3', question_5:'3', question_6:'4', question_7:'4', question_8:'4',category: 'S'}
];

jms_data = _(jms_data);
var jms_features = ['question_2', 'question_3', 'question_4', 'question_5', 'question_6', 'question_7', 'question_8'];
var jms_samples = [
    { question_2: '0', question_3: '0', question_4: '0', question_5: '0', question_6: '0', question_7: '0', question_8: '0', category: 'J' },
    { question_2: '1', question_3: '2', question_4: '3', question_5: '4', question_6: '3', question_7: '2', question_8: '1', category: 'J' },
    { question_2: '3', question_3: '2', question_4: '3', question_5: '3', question_6: '2', question_7: '4', question_8: '4', category: 'M' },
    { question_2: '3', question_3: '1', question_4: '3', question_5: '4', question_6: '1', question_7: '2', question_8: '2', category: 'M' },
    { question_2: '3', question_3: '3', question_4: '2', question_5: '4', question_6: '2', question_7: '1', question_8: '2', category: 'J' },
    { question_2: '4', question_3: '1', question_4: '3', question_5: '4', question_6: '2', question_7: '4', question_8: '2', category: 'M' },
    { question_2: '4', question_3: '2', question_4: '3', question_5: '2', question_6: '4', question_7: '3', question_8: '2', category: 'S' },
    { question_2: '3', question_3: '3', question_4: '3', question_5: '3', question_6: '3', question_7: '3', question_8: '4', category: 'S' },
    { question_2: '4', question_3: '2', question_4: '4', question_5: '2', question_6: '4', question_7: '4', question_8: '2', category: 'M' },
    { question_2: '4', question_3: '0', question_4: '0', question_5: '4', question_6: '0', question_7: '4', question_8: '0', category: 'J' },
    { question_2: '4', question_3: '2', question_4: '2', question_5: '3', question_6: '0', question_7: '3', question_8: '3', category: 'M' }
];