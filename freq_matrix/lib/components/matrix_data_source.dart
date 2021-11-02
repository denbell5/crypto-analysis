import 'dart:io';

import 'package:flutter/material.dart';
import 'package:freq_matrix/components/matrix.dart';

class Gram {
  final String key;
  final double value;

  Gram(this.key, this.value);
}

class MatrixDataSource extends StatefulWidget {
  const MatrixDataSource({Key? key}) : super(key: key);

  @override
  _MatrixDataSourceState createState() => _MatrixDataSourceState();
}

class _MatrixDataSourceState extends State<MatrixDataSource> {
  String baseDirPath = 'C:\\univ\\4_1\\zah\\lab-4\\texts';
  String textName = 'text1';
  String get baseTextPath => '$baseDirPath\\$textName\\$textName';
  String get alphabetPath => '$baseTextPath-alphabet.txt';
  String get keysPath => '$baseTextPath-result-2-by-frequency-keys.txt';
  String get valuesPath => '$baseTextPath-result-2-by-frequency-values.txt';

  String alphabet = '';
  List<String> keys = [];
  List<String> values = [];

  Future<void> readValues() async {
    var file = File(alphabetPath);
    alphabet = await file.readAsString();

    file = File(keysPath);
    keys = await file.readAsLines();

    file = File(valuesPath);
    values = await file.readAsLines();
    setState(() {});
  }

  List<Gram> _parseGrams() {
    final grams = <Gram>[];
    if (keys.isEmpty) return grams;
    for (var i = 0; i < keys.length; i++) {
      grams.add(
        Gram(
          keys[i],
          double.parse(values[i]),
        ),
      );
    }
    return grams;
  }

  @override
  Widget build(BuildContext context) {
    final rawScreenSize = MediaQuery.of(context).size;
    const padding = 10.0;
    final screenSize = Size(
      rawScreenSize.width - padding,
      rawScreenSize.height - padding,
    );
    final tableSize = Size(screenSize.height, screenSize.height);
    final configPanelSize = Size(
      screenSize.width - tableSize.height,
      screenSize.height,
    );
    final grams = _parseGrams();
    return Scaffold(
      body: Padding(
        padding: const EdgeInsets.all(padding / 2),
        child: Row(
          children: [
            SizedBox(
              height: configPanelSize.height,
              width: configPanelSize.width,
              child: _buildConfigPanel(),
            ),
            SizedBox(
              height: tableSize.height,
              width: tableSize.width,
              child: Matrix(
                size: tableSize,
                grams: grams,
                alphabet: alphabet,
              ),
            ),
          ],
        ),
      ),
    );
  }

  Widget _buildConfigPanel() {
    const divider = SizedBox(height: 10);
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        _buildInput(
          'Base directory',
          baseDirPath,
          (String? value) {
            baseDirPath = value!;
          },
        ),
        _buildInput(
          'Text name',
          textName,
          (String? value) {
            textName = value!;
          },
        ),
        TextButton(
          onPressed: () {
            readValues();
          },
          child: const Text('Read'),
        ),
        divider,
        if (alphabet.isEmpty || keys.isEmpty || values.isEmpty)
          const Text('Data is empty')
        else
          const Text('Data is read'),
        divider,
        const Text('Base text path'),
        Text(baseTextPath),
        divider,
        const Text('Alphabet path'),
        Text(alphabetPath),
        divider,
        const Text('Keys path'),
        Text(keysPath),
        divider,
        const Text('Values path'),
        Text(valuesPath),
      ],
    );
  }

  Widget _buildInput(
    String label,
    String initialValue,
    void Function(String?) onChanged,
  ) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        Text(label),
        _wrapWithBorder(
          TextFormField(
            initialValue: initialValue,
            onChanged: onChanged,
          ),
        ),
      ],
    );
  }

  Widget _wrapWithBorder(Widget child) {
    return Container(
      decoration: BoxDecoration(
        border: Border.all(),
      ),
      child: child,
    );
  }
}
